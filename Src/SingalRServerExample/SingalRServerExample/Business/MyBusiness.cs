using Microsoft.AspNetCore.SignalR;
using SingalRServerExample.Hubs;

namespace SingalRServerExample.Business
{
    public class MyBusiness
    {
        //IHubContext interface i bizden temsil edeceği hub sınıfını ister.Burada dolaylı yoldan myhuba baglanıp
        //Elime bir context gelicek gelen bu context uzerinden dolaylı yoldan myhuba bağlanıp ileti gönderilecek

        //Businessta yapmamzıın asıl amacı farklı işlevler eklenmesi durumda buradan o işlemleri yaptıramk mantıklıdır ama böyle algoritmik fonsksiyonlu işlemelr yok ise gerek yoktur.
        readonly IHubContext<MyHub> _hubContext;
        public MyBusiness(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        //Hubn sınıfında olan sendemessage methodumuzu business sınıfımıza ekledik
        public async Task SendMessageAsync(string message)
        {
            //Clients Hub sınıfından gelir.SendAsync , clientlarda hangi methodun tetikleneceğini ilk parametresinde ister ve clientın gönderdiği mesajı ister.
            await _hubContext.Clients.All.SendAsync("receiveMessage", message);
        }



    }
}
