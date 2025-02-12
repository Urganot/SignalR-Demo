namespace SignalR_Demo.Web.Components.Pages
{
    using Microsoft.AspNetCore.SignalR.Client;

    public partial class Chat
    {
        private HubConnection? hubConnection;
        private List<string> receivedMessages = [];
        private string userInput = "";
        private string messageInput = "";

        protected override async Task OnInitializedAsync()
        {
            var backendUrl = Configuration["services:apiservice:https:0"];
            hubConnection = new HubConnectionBuilder()
                .WithUrl(backendUrl + "/chatHub") // Change to your backend URL
                .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                receivedMessages.Add($"{user}: {message}");
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        private void SendMessage()
        {
            if (hubConnection is not null)
            {
                hubConnection.InvokeAsync("SendMessage", userInput, messageInput);
            }
        }
    }
}