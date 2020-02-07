using CQRSlite.Commands;
using Nancy;
using Nancy.Security;
using org.neurul.Common.Domain.Model;
using works.ei8.Cortex.Diary.Nucleus.Application.Neurons.Commands;
using works.ei8.Cortex.Diary.Nucleus.Port.Adapter.Common;
using System;
using System.Linq;

namespace works.ei8.Cortex.Diary.Nucleus.Port.Adapter.In.Api
{
    public class NeuronModule : NancyModule
    {
        public NeuronModule(ICommandSender commandSender) : base("/{avatarId}/nuclei/d23/neurons")
        {
            this.Post(string.Empty, async (parameters) =>
            {
                return await Helper.ProcessCommandResponse(
                        commandSender,
                        this.Request,
                        false,
                        (bodyAsObject, bodyAsDictionary, expectedVersion) =>
                        {
                            return new CreateNeuron(
                                parameters.avatarId,
                                Guid.Parse(bodyAsObject.Id.ToString()),
                                bodyAsObject.Tag.ToString(),
                                Guid.Parse(bodyAsObject.RegionId.ToString()),
                                Guid.Parse(bodyAsObject.AuthorId.ToString())
                                );                            
                        },
                        "Id",
                        "Tag",
                        "RegionId",
                        "AuthorId"
                    );
            }
            );

            this.Patch("/{neuronId}", async (parameters) =>
            {
                return await Helper.ProcessCommandResponse(
                        commandSender,
                        this.Request,
                        (bodyAsObject, bodyAsDictionary, expectedVersion) =>
                        {
                            return new ChangeNeuronTag(
                                parameters.avatarId,
                                Guid.Parse(parameters.neuronId),
                                bodyAsObject.Tag.ToString(),
                                Guid.Parse(bodyAsObject.AuthorId.ToString()),
                                expectedVersion
                                );
                        },
                        "Tag",
                        "AuthorId"
                    );
            }
            );

            this.Delete("/{neuronId}", async (parameters) =>
            {
                return await Helper.ProcessCommandResponse(
                        commandSender,
                        this.Request,
                        (bodyAsObject, bodyAsDictionary, expectedVersion) =>
                        {
                            return new DeactivateNeuron(
                                parameters.avatarId,
                                Guid.Parse(parameters.neuronId),
                                Guid.Parse(bodyAsObject.AuthorId.ToString()),
                                expectedVersion
                                );
                        },
                        "AuthorId"
                    );
            }
            );
        }
    }
}
