// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.IO;

namespace Searcher
{
    public interface IResourceHolder : IDisposable
    {
        Stream GetResourceStream();
    }
}