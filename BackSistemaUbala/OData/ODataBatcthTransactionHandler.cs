using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Arkeos.Support
{
    public class ODataBatchTransactionHandler : DefaultODataBatchHandler
    {
        //public override async Task ProcessBatchAsync(HttpContext context, RequestDelegate nextHandler)
        //{
        //    using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //    {
        //        await base.ProcessBatchAsync(context, nextHandler);
        //        //scope.Complete();
        //    }
        //}

        public override async Task ProcessBatchAsync(HttpContext context, RequestDelegate nextHandler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (nextHandler == null)
            {
                throw new ArgumentNullException(nameof(nextHandler));
            }

            if (!await ValidateRequest(context.Request).ConfigureAwait(false))
            {
                return;
            }

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                IList<ODataBatchRequestItem> subRequests = await ParseBatchRequestsAsync(context).ConfigureAwait(false);

                // ODataOptions options = context.RequestServices.GetRequiredService<ODataOptions>();
                ODataOptions options = context.RequestServices.GetRequiredService<IOptions<ODataOptions>>().Value;
                bool enableContinueOnErrorHeader = (options != null)
                    ? options.EnableContinueOnErrorHeader
                    : false;

                //SetContinueOnError(context.Request.Headers, enableContinueOnErrorHeader);

                IList<ODataBatchResponseItem> responses = await ExecuteRequestMessagesAsync(subRequests, nextHandler).ConfigureAwait(false);

                await CreateResponseMessageAsync(responses, context.Request).ConfigureAwait(false);

                var badResponses = responses.Count((x) => ((OperationResponseItem)x).Context.Response.StatusCode < 200 || ((OperationResponseItem)x).Context.Response.StatusCode >= 300);
                if (badResponses == 0)
                {
                    scope.Complete();
                }
            }
        }
        
    }
}
