using Microsoft.AspNetCore.SignalR;

namespace SingalRServerExample.Hubs
{

    //Sınıfın ismine sadece sonuna hub yazmak yetmez ,  birde Hub sınıfından kalıtım alması gerekir. 
    public class MyHub :Hub
    {
        //Kendi fonksiyonumuz.Gelen mesajı karşılayacak
        public async Task SendMessageAsync(string message)
        {
            //Clients Hub sınıfından gelir.SendAsync , clientlarda hangi methodun tetikleneceğini ilk parametresinde ister ve clientın gönderdiği mesajı ister.
           await  Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
