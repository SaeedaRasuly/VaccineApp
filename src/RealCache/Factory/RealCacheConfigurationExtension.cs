﻿using Core.Models;
using RealCache.Persistence.Migrations;

namespace RealCache.Factory;

public static class RealCacheConfigurationExtension
{
    public static IServiceCollection AddRealCache(
        this IServiceCollection service)
    {
        service.AddSingleton<DbContext<PeriodModel>>();

        return service;
    }
}