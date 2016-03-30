# Node-Monitor
.Net Remoting Server-Client Application collects server Ram and Processor usage and send it to client 

Tools and Technology 
  .Net Framework 4.5 
  C# 
  Visual Studio 2013 
  Unit Testing 
  StructureMap. 
  Windows Forms 
  .Net Remoting 

Architecture 
 NodeMonitor.Client:   
windows forms project connect to server using (.Net Remoting object) every one second to collect 
Samples which chosen by user and displayed it in Data Grid. 
 NodeMonitor.Server : 
Console Application create .Net Remoting object and collect registered samples types every one 
second, stored it in memory and discard old samples data. 
In the beginning of app, I register all data collectors using StructureMap.   
  NodeMonitor.SamplesReaders:   
Class library project act as the business layer or logic layer, every sample data should added and 
implement Interface ISampleReader. 
Every sample data should have sampleTypeId (Key) 
  NodeMonitor.SamplesStore:   
Class library project act as the service layer which cache and store all samples data in Memory. Server 
collector invoked it directly to request, remove and update samples data. 
I used key/value collection ConcurrentDictionary to store data with key the sampleTypeId 
and value SampleDataModel 


Expandability: 
  I create SampleReadersUnitOfWork as a middle point for all samples.  
If you want to create new sample type, register it in Dependency Injection configuration class StructureMapConfigure with ISampleReader Interface and it will works automatically. 


Performance: 
  All Sampled Data saved in Memory, so it serving clients with High performance and quick response. 
  SamplesCollector which fetch data is a Singleton object.  
