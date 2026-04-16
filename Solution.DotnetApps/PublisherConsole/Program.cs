using PublisherDataAccess;
var dataContext = new PubDBContext();

dataContext.Database.EnsureCreated();
var isConnected = await dataContext.Database.CanConnectAsync();
Console.WriteLine(isConnected);