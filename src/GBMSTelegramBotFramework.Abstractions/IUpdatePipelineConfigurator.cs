﻿using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdatePipelineConfigurator
{
    IServiceCollection Services { get; }
    IUpdatePipelineConfigurator Configure(Action<IUpdatePipelineBuilder> configure);
    void ConfigureBuilder(IUpdatePipelineBuilder builder);
}