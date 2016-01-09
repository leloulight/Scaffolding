// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.CodeGeneration;
using Microsoft.Extensions.CodeGeneration.CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.CodeGenerators.Mvc.Controller
{
    [Alias("controller")]
    public class CommandLineGenerator : ICodeGenerator
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandLineGenerator(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            _serviceProvider = serviceProvider;
        }

        public async Task GenerateCode(CommandLineGeneratorModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            ControllerGeneratorBase generator = null;

            if (string.IsNullOrEmpty(model.ModelClass))
            {
                if (model.GenerateReadWriteActions)
                {
                    generator = GetGenerator<MvcControllerWithReadWriteActionGenerator>();
                }
                else
                {
                    generator = GetGenerator<MvcControllerEmpty>(); //This need to handle the WebAPI Empty as well...
                }
            }
            else
            {
                generator = GetGenerator<ControllerWithContextGenerator>();
            }

            if (generator != null)
            {
                await generator.Generate(model);
            }
        }

        private ControllerGeneratorBase GetGenerator<TChild>() where TChild : ControllerGeneratorBase
        {
            return (ControllerGeneratorBase)ActivatorUtilities.CreateInstance<TChild>(_serviceProvider);
        }
    }
}
