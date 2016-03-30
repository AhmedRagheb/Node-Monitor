using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using NodeMonitor.Models;

namespace NodeMonitor.Server.SamplesCollector
{
    public interface ISamplesCollector
    {
        List<SampleDataModel> Fetch(params int[] sampleTypeId);
        List<SampleType> GetSampleTypes();
        object GetLifetimeService();
        object InitializeLifetimeService();
        ObjRef CreateObjRef(Type requestedType);
    }
}