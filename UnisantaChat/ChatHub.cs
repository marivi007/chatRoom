using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace UnisantaChat
{
    public class ChatHub : Hub
    {
        static ConcurrentDictionary<string, string> dic = new ConcurrentDictionary<string, string>();
        //adicionar mensagem para todos os usuarios
        public void Send(string apelido, string mensagem,int privado)
        {

            Clients.All.adicionarNovaMensagem(apelido, mensagem,privado);
        }

        //Enviar mensagem para um usuario só
        public void SendToSpecific(string apelido, string mensagem, string user,int privado)
        {
            Clients.Caller.adicionarNovaMensagem(apelido, mensagem);
            Clients.Client(dic[user]).adicionarNovaMensagem(apelido, mensagem,privado);
        }

        //Entrada de novo usuario
        public void Notify(string name, string id)
        {
            if (dic.ContainsKey(name))
            {
                Clients.Caller.differentName();
            }
            else
            {
                dic.TryAdd(name, id);
                foreach (KeyValuePair<String, String> entry in dic)
                {
                    Clients.Caller.online(entry.Key);
                }
                Clients.Others.enters(name);
            }
        }

        //Usuario desconectado
        public override Task OnDisconnected(bool stopCalled)
        {
            var name = dic.FirstOrDefault(x => x.Value == Context.ConnectionId.ToString());
            string s;
            dic.TryRemove(name.Key, out s);
            return Clients.All.disconnected(name.Key);
        }
    }
}