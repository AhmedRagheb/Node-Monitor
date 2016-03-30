# Node-Monitor
.Net Remoting Server-Client Application collects server Ram and Processor usage and send it to client 

Tools and Technology
 .Net Framework 4.5
  C# 
  StructureMap.
  Windows Forms
  .Net Remoting


Expandability: 
  I create SampleReadersUnitOfWork as a middle point for all samples.  
If you want to create new sample type, register it in Dependency Injection configuration class StructureMapConfigure with ISampleReader Interface and it will works automatically. 


Performance: 

  All Sampled Data saved in Memory, so it serving clients with High performance and quick response. 
  SamplesCollector which fetch data is a Singleton object.  
