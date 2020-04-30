using Nancy;
using Nancy.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ei8.Cortex.Diary.Common;
using ei8.Cortex.Diary.Nucleus.Application.Neurons;

namespace ei8.Cortex.Diary.Nucleus.Port.Adapter.Out.Api
{
    public class NeuronModule : NancyModule
    {
        private const string DefaultLimit = "1000";
        private const string DefaultType = "NotSet";

        public NeuronModule(INeuronQueryService queryService) : base("/nuclei/d23/neurons")
        {
            this.Get("", async (parameters) =>
            {
                return await NeuronModule.ProcessRequest(async () =>
                {
                    var limit = this.Request.Query["limit"].HasValue ? this.Request.Query["limit"].ToString() : NeuronModule.DefaultLimit;

                    var nv = await queryService.GetNeurons(neuronQuery: NeuronModule.ExtractQuery(this.Request.Query), limit: int.Parse(limit));
                    return new TextResponse(JsonConvert.SerializeObject(nv));
                }
                );
            }
            );

            this.Get("/{neuronid:guid}", async (parameters) =>
            {
                return await NeuronModule.ProcessRequest(async () =>
                {
                    var nv = await queryService.GetNeuronById(parameters.neuronid);
                    return new TextResponse(JsonConvert.SerializeObject(nv));
                }
                );
            }
            );

            this.Get("/{centralid:guid}/relatives", async (parameters) =>
            {
                return await NeuronModule.ProcessRequest(async () =>
                {
                    var type = this.Request.Query["type"].HasValue ? this.Request.Query["type"].ToString() : NeuronModule.DefaultType;
                    var limit = this.Request.Query["limit"].HasValue ? this.Request.Query["limit"].ToString() : NeuronModule.DefaultLimit;

                    var nv = await queryService.GetNeurons(
                        parameters.centralid,
                        Enum.Parse(typeof(RelativeType), type),
                        NeuronModule.ExtractQuery(this.Request.Query),
                        int.Parse(limit)
                        );

                    return new TextResponse(JsonConvert.SerializeObject(nv));
                }
                );
            }
            );

            this.Get("/{centralid:guid}/relatives/{neuronid:guid}", async (parameters) =>
            {
                return await NeuronModule.ProcessRequest(async () =>
                {
                    var type = this.Request.Query["type"].HasValue ? this.Request.Query["type"].ToString() : NeuronModule.DefaultType;

                    var nv = await queryService.GetNeuronById(
                        parameters.neuronid,
                        parameters.centralid,
                        Enum.Parse(typeof(RelativeType), type)
                        );
                    return new TextResponse(JsonConvert.SerializeObject(nv));
                }
                );
            }
            );
        }

        private static NeuronQuery ExtractQuery(dynamic query)
        {
            var nq = new NeuronQuery();
            nq.TagContains = NeuronModule.GetQueryArrayOrDefault(query, nameof(NeuronQuery.TagContains));
            nq.TagContainsNot = NeuronModule.GetQueryArrayOrDefault(query, nameof(NeuronQuery.TagContainsNot));
            nq.Id = NeuronModule.GetQueryArrayOrDefault(query, nameof(NeuronQuery.Id));
            nq.IdNot = NeuronModule.GetQueryArrayOrDefault(query, nameof(NeuronQuery.IdNot));
            nq.Postsynaptic = NeuronModule.GetQueryArrayOrDefault(query, nameof(NeuronQuery.Postsynaptic));
            nq.PostsynapticNot = NeuronModule.GetQueryArrayOrDefault(query, nameof(NeuronQuery.PostsynapticNot));
            nq.Presynaptic = NeuronModule.GetQueryArrayOrDefault(query, nameof(NeuronQuery.Presynaptic));
            nq.PresynapticNot = NeuronModule.GetQueryArrayOrDefault(query, nameof(NeuronQuery.PresynapticNot));
            return nq;
        }

        private static IEnumerable<string> GetQueryArrayOrDefault(dynamic query, string parameterName)
        {
            var parameterNameExclamation = parameterName.Replace("Not", "!");
            return query[parameterName].HasValue ?
                query[parameterName].ToString().Split(",") :
                    query[parameterNameExclamation].HasValue ?
                    query[parameterNameExclamation].ToString().Split(",") :
                    null;
        }

        internal static async Task<Response> ProcessRequest(Func<Task<Response>> action)
        {
            var result = new Response { StatusCode = HttpStatusCode.OK };

            try
            {
                result = await action();
            }
            catch (Exception ex)
            {
                result = new TextResponse(HttpStatusCode.BadRequest, ex.ToString());
            }

            return result;
        }
    }
}
