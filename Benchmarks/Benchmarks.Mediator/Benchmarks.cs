using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using BenchmarkDotNet.Reports;

namespace Benchmarks.Mediator
{
    [MemoryDiagnoser]
    [Config(typeof(CustomConfig))]
    public class Benchmarks
    {
        private MediatR.ISender _mediatRSender;
        private ISender _mediatorSender;

        [GlobalSetup]
        public void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddMediatR(configuration => 
            {
                configuration.Lifetime = ServiceLifetime.Singleton;
                configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);

            });


            services.AddMediator(options => 
            {
                options.ServiceLifetime = ServiceLifetime.Singleton;
                options.Namespace = "Benchmarks.Mediator.Models";
            });


            var serviceProvider = services.BuildServiceProvider();

            _mediatRSender = serviceProvider.GetRequiredService<MediatR.ISender>();
            _mediatorSender = serviceProvider.GetRequiredService<ISender>();
        }

        [Benchmark(Baseline =true)]
        public async Task PingMediatR()
        {
            var request = new MediatRRequests("Ping MediatR");
            var response = await _mediatRSender.Send(request);
        }

        [Benchmark]
        public async Task PingMediator() 
        {
            var request = new MediatorRequests("Ping Mediator");
            var response=await _mediatorSender.Send(request);
        }

        private class CustomConfig : ManualConfig
        {
            public CustomConfig()
            {
                SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
            }
        }

    }
}
