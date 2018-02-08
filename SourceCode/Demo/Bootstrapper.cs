using Autofac;
using Demo.Rates;

namespace Demo
{
    /// <summary>
    ///     Registers the dependencies
    /// </summary>
    internal static class Bootstrapper
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<RateSelector>().As<IRateSelector>().SingleInstance();
            builder.RegisterType<PriceCalculator>().As<IPriceCalculator>().SingleInstance();
            // Registers all the rates
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .As<RateBase>()
                .AssignableTo<RateBase>()
                .SingleInstance();
            return builder.Build();
        }
    }
}