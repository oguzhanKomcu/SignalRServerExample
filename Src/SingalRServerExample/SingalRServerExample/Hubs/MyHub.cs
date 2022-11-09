using Microsoft.AspNetCore.SignalR;

namespace SingalRServerExample.Hubs
{

    //Sınıfın ismine sadece sonuna hub yazmak yetmez ,  birde Hub sınıfından kalıtım alması gerekir. 
    public class MyHub :Hub
    {
        //giriş yapan clientları koleksiyonumuza ekliyoruz.
        static List<string> clients = new List<string>();   


        //Kendi fonksiyonumuz.Gelen mesajı karşılayacak
        public async Task SendMessageAsync(string message)
        {
            //Clients Hub sınıfından gelir.SendAsync , clientlarda hangi methodun tetikleneceğini ilk parametresinde ister ve clientın gönderdiği mesajı ister.
           await  Clients.All.SendAsync("receiveMessage", message);
        }



        //Hub üzerinden aynı yere bağlanan clientların , clientların bağlantısı kopması durumunda haberdar olması gibi senaryolar için burada bağlantı --
        //eventleri ile ilgili fonksiyonlarımızı oluşturmamız gerekiyor.
        //Bunun için Hub sınıfındna gelen methodları override ediyoruz.

        public override async Task OnConnectedAsync()
        {
            //Bu fonksiyonlar sayesinde Clienta dair bilgileride yakalyabiliriz.Örn:Context.ConnectionID özelliği. ConnectionID , huba bağlantı gerçekleştiren clientlara sistem tarafından verilen unique/tekil bir değerdir.Bu sekilde clientlar birbirinden ayrılmıs olur.
            //Tüm clientlara gönderilir.Clint tarafındaki çalışacak methodu veriyoruz ve ConnectionID'yi
            
            
            clients.Add(Context.ConnectionId);//clientı yakalayıp listeye IDsini ekledik.
            await Clients.All.SendAsync("clients",clients); //mevcut tüm clientları haberdar edicek yani client tarafında bu method çalıştıırlacak diyorum.
            await Clients.All.SendAsync("userJoined", Context.ConnectionId);
       
            

        }


        //1.var olan bir bağlantı oldugunda ve sayfa yenilemek istediğimizde önce  OnDisconnectedAsync() olup sonrasında OnConnectedAsync() methodu çalıştırılacaktır.
        public override async Task OnDisconnectedAsync(Exception? exception)
        {

            clients.Add(Context.ConnectionId);//clientı yakalayıp listeye IDsini ekledik.
            await Clients.All.SendAsync("clients", clients); //mevcut tüm clientları haberdar edicek yani client tarafında bu method çalıştıırlacak diyorum.

            await Clients.All.SendAsync("userLeaved", Context.ConnectionId);


        }

    }
}
