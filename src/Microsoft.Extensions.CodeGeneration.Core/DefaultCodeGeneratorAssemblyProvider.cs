// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Microsoft.Extensions.CodeGeneration
{
    public class DefaultCodeGeneratorAssemblyProvider : ICodeGeneratorAssemblyProvider
    {
        private static readonly HashSet<string> _codeGenerationFrameworkAssemblies =
            new HashSet<string>(StringComparer.Ordinal)
            {
                "Microsoft.Extensions.CodeGeneration",
            };

        private readonly ILibraryManager _libraryManager;

        public DefaultCodeGeneratorAssemblyProvider(ILibraryManager libraryManager)
        {
            if (libraryManager == null)
            {
                throw new ArgumentNullException(nameof(libraryManager));
            }

            _libraryManager = libraryManager;
        }

        public IEnumerable<Assembly> CandidateAssemblies
        {
            get
            {
                return _codeGenerationFrameworkAssemblies
                    .SelectMany(_libraryManager.GetReferencingLibraries)
                    .Distinct()
                    .Where(IsCandidateLibrary)
                    .Select(lib => Assembly.Load(new AssemblyName(lib.Name)));
            }
        }

        private bool IsCandidateLibrary(Library library)
        {
            return !_codeGenerationFrameworkAssemblies.Contains(library.Name);
        }
    }
}