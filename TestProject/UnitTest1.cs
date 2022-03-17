using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FakeItEasy;
using HttpClientTesting.Client;
using HttpClientTesting.Service;
using Shouldly;
using Xunit;

namespace TestProject;

public class UnitTest1
{
    [Fact]
    public async Task Test_Init_Service()
    {

        var client = A.Fake<ISimpleHttpClient>();
        var service = new SimpleService(client);

        var result = await service.GetDataAsync();

        result.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task Test_InternalServer_Error_Response()
    {
        // arrange
        var client = A.Fake<ISimpleHttpClient>();
        var service = new SimpleService(client);
        
        var response = new HttpResponseMessage(HttpStatusCode.InternalServerError){ Content = new StringContent("Failed")};
        A.CallTo(() => client.GetAsync()).Returns(Task.FromResult(response));
        
        // act

        var result = await service.GetDataAsync();
        
        // assert
        A.CallTo(() => client.GetAsync()).MustHaveHappened();
        
        
        result.ShouldNotBeNull();
        result.Success.ShouldBeFalse();
        result.ResponseDate.ShouldNotBe(DateTime.MinValue);
        result.Message.IsSuccessStatusCode.ShouldBeFalse();
    }
    
    [Fact]
    public async Task Test_Ok_Response()
    {
        // arrange
        var client = A.Fake<ISimpleHttpClient>();
        var service = new SimpleService(client);
        
        var response = new HttpResponseMessage(HttpStatusCode.OK){ Content = new StringContent("Worked")};
        A.CallTo(() => client.GetAsync()).Returns(Task.FromResult(response));
        
        // act

        var result = await service.GetDataAsync();
        
        // assert
        A.CallTo(() => client.GetAsync()).MustHaveHappened();
        
        
        result.ShouldNotBeNull();
        result.Success.ShouldBeTrue();
        result.ResponseDate.ShouldNotBe(DateTime.MinValue);
        result.Message.IsSuccessStatusCode.ShouldBeTrue();
    }
}