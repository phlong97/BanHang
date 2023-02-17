using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ban_Hang
{
    internal class FireBaseDB
    {
        public FirebaseClient Client;
        public FireBaseDB(string URL, string Secret)
        {
            Client = new FirebaseClient(
              URL,
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(Secret)
              });
        }
        public async Task UpdateToFirebase<T>(string child, T data) where T : MiliObject
        {
            if (!string.IsNullOrEmpty(data.Id))
            {
                await Client.Child(child).Child(data.Id).DeleteAsync();
            }
            data.Id = MiliHelper.CreateKey();
            data.Sync = true;
            await Client.Child(child).Child(data.Id).PutAsync(data);
            //Luu key vao lang nghe
            //SaveToObserv(data.Key);
        }


        public async Task<List<T>> GetDataFromFireBase<T>(string child) where T : MiliObject
        {
            var data = await
                 Client.Child(child).OnceAsync<T>();

            return data.Select(x => x.Object).ToList();
        }
    }
}
