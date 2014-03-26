namespace Squire.Unhinged.Pipeline
{
    using Squire.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PipelineBuilder
    {
        private readonly List<DownstreamContext> downstreamHandlers;

        private readonly List<UpstreamContext> upstreamHandlers;

        public PipelineBuilder()
        {
            this.downstreamHandlers = new List<DownstreamContext>();
            this.upstreamHandlers = new List<UpstreamContext>();
        }

        public void RegisterUpstream(IUpstreamHandler handler)
        {
            ValidationHelper.ArgumentNotNull(handler, "handler");
            var context = new UpstreamContext(handler);
            if (this.upstreamHandlers.Count > 0)
            {
                this.upstreamHandlers[this.upstreamHandlers.Count - 1].SetNext(context);
            }

            this.upstreamHandlers.Add(context);
        }

        public void RegisterDownstream(IDownstreamHandler handler)
        {
            ValidationHelper.ArgumentNotNull(handler, "handler");
            var context = new DownstreamContext(handler);
            if (this.downstreamHandlers.Count > 0)
            {
                this.downstreamHandlers[this.downstreamHandlers.Count - 1].SetNext(context);
            }

            this.downstreamHandlers.Add(context);
        }

        public IPipeline Build()
        {
            if (this.upstreamHandlers.Count <= 0)
            {
                throw new InvalidOperationException("There needs to be at least one upstream handler.");
            }

            if (this.downstreamHandlers.Count <= 0)
            {
                throw new InvalidOperationException("There needs to be at least one downstream handler.");
            }

            var pipeline = new Pipeline();
            foreach (var upstreamHandler in this.upstreamHandlers)
            {
                upstreamHandler.SetDownstream(this.downstreamHandlers[0]);
                pipeline.Add(upstreamHandler);
            }

            foreach (var downstreamHandler in this.downstreamHandlers)
            {
                downstreamHandler.SetUpstream(this.upstreamHandlers[0]);
                pipeline.Add(downstreamHandler);
            }

            return pipeline;
        }
    }
}
