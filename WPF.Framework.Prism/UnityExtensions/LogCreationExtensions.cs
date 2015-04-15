﻿using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using WPF.Framework.Infrastructure.Services;

namespace WPF.Framework.Prism.UnityExtensions
{
    public class LogCreation : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.AddNew<LogCreationStrategy>(UnityBuildStage.PreCreation);
        }
    }

    public class LogCreationStrategy : BuilderStrategy
    {
        public bool IsPolicySet { get; private set; }

        public override void PreBuildUp(IBuilderContext context)
        {
            var typeToBuild = context.BuildKey.Type;
            if (typeof (ILoggerFacade) != typeToBuild) return;
            if (context.Policies.Get<IBuildPlanPolicy>(context.BuildKey) != null) return;
            var typeForLog = GetLogType(context);
            var policy = new LogBuildPlanPolicy(typeForLog);
            context.Policies.Set<IBuildPlanPolicy>(policy, context.BuildKey);

            IsPolicySet = true;
        }

        public override void PostBuildUp(IBuilderContext context)
        {
            if (!IsPolicySet) return;
            context.Policies.Clear<IBuildPlanPolicy>(context.BuildKey);
            IsPolicySet = false;
        }

        private static Type GetLogType(IBuilderContext context)
        {
            Type logType = null;
            var buildTrackingPolicy = BuildTracking.GetPolicy(context);
            if ((buildTrackingPolicy != null) && (buildTrackingPolicy.BuildKeys.Count >= 2))
            {
                logType = ((NamedTypeBuildKey) buildTrackingPolicy.BuildKeys.ElementAt(1)).Type;
            }
            else
            {
                var stackTrace = new StackTrace();
                //first two are in the log creation strategy, can skip over them
                for (var i = 2; i < stackTrace.FrameCount; i++)
                {
                    var frame = stackTrace.GetFrame(i);
                    logType = frame.GetMethod().DeclaringType;
                    if (logType != null && !logType.FullName.StartsWith("Microsoft.Practices"))
                    {
                        break;
                    }
                }
            }
            return logType;
        }
    }

    public class LogBuildPlanPolicy : IBuildPlanPolicy
    {

        public LogBuildPlanPolicy(Type logType)
        {
            LogType = logType;
        }

        public Type LogType { get; private set; }

        public void BuildUp(IBuilderContext context)
        {
            if (context.Existing != null) return;
            ILoggerFacade log = new LoggerService(LogType.FullName);
            context.Existing = log;
        }
    }
}