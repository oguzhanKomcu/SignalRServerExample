using Microsoft.AspNetCore.SignalR;

namespace SingalRServerExample.Hubs
{
    //Bu messageHub sınıfımızı oluşturduktan sonra unutmamak gerekir ki programcs de bunu tanımlamamız gerekmektedir...
    public class MessageHub :Hub
    {
        public async Task SendMessageAsync(string message)
        {
            #region Caller
            //Sadece servera bildirim gönderen client la iletişim kurar ve diğer clientlara bildirim gitmez
            await Clients.Caller.SendAsync("receiveMessage", message);

            #endregion
            #region All


            #endregion
            #region Other


            #endregion




        }

    }
}
