﻿using Grpc.Core;
using GrpcService.Protos;

namespace GrpcService.Services
{
    public class StreamService : GrpcService.Protos.GrpcStream.GrpcStreamBase
    {
        public override async Task FromServer(Request request, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            for (int i = 0; i < int.MaxValue && !context.CancellationToken.IsCancellationRequested; i++)
            {
                await responseStream.WriteAsync(new Response { Text = request.Text });
            }
        }

        public override async Task<Response> ToServer(IAsyncStreamReader<Request> requestStream, ServerCallContext context)
        {
            var response = new Response();
            /*while(await requestStream.MoveNext())
            {
                response.Text += requestStream.Current;
            }*/

            await foreach (var request in requestStream.ReadAllAsync())
            {
                response.Text += request.Text;
            }

            return response;
        }

        public override async Task FromAndToServer(IAsyncStreamReader<Request> requestStream, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            await foreach (var request in requestStream.ReadAllAsync())
            {
                await responseStream.WriteAsync(new Response { Text = "-" + request.Text });
            }
        }
    }
}
