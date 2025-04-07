using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Model
{
    internal class Repository
    {
        HttpClient httpClient;

        readonly string ErrorMessage = "Error en l'API.";
        readonly string contentType = "application/json";

        public void CreateHttpClient()  // Cal executar aquest mètode en el constructor del Controller
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("")
            };
            httpClient.DefaultRequestHeaders.Add("Accept", contentType);
        }

        //static DateTime? ConvertToDateTime(string input)
        //{
        //    if (DateTime.TryParseExact(input, " MMMM d, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        //    {
        //        return result;
        //    }
        //    else if (DateTime.TryParseExact(input, "MMMM d, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result1))
        //    {
        //        return result1;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public List<Game> GetGames()
        //{
        //    List<Game> games = null;
        //    Results<Game> result = null;
        //    try
        //    {
        //        result = (Results<Game>)MakeRequest("games?limit=-1", "GET", null, typeof(Results<Game>)).Result;
        //        games = result.data;
        //        foreach (Game game in games)
        //        {
        //            game.date_user = ConvertToDateTime(game.released_date);
        //            if (game.date_user != null)
        //            {
        //                game.released_date = game.date_user.Value.ToShortDateString();
        //            }
        //            game.staff_count = getCountStaff(game.id).ToString();
        //            if (game.staff_count == "0")
        //            {
        //                game.staff_count = "N/A";
        //            }
        //        }
        //        games = games.OrderBy(x => x.date_user).ToList();
        //    }
        //    catch { }
        //    if (games == null)
        //    {
        //        games = new List<Game>();
        //    }
        //    return games;
        //}

        //public List<Game> GetGamesFilter(string filter)
        //{
        //    List<Game> games = null;
        //    Results<Game> result = null;
        //    try
        //    {
        //        result = (Results<Game>)MakeRequest("games?limit=-1&name=" + filter, "GET", null, typeof(Results<Game>)).Result;
        //        games = result.data;
        //        foreach (Game game in games)
        //        {
        //            game.date_user = ConvertToDateTime(game.released_date);
        //            if (game.date_user != null)
        //            {
        //                game.released_date = game.date_user.Value.ToShortDateString();
        //            }
        //            game.staff_count = getCountStaff(game.id).ToString();
        //        }
        //        games = games.OrderBy(x => x.date_user).ToList();
        //    }
        //    catch { }
        //    if (games == null)
        //    {
        //        games = new List<Game>();
        //    }
        //    return games;
        //}
        //public List<Staff> GetStaff(string gameId)
        //{
        //    List<Staff> staff = null;
        //    List<Staff> staff1 = new List<Staff>();
        //    Results<Staff> result = null;
        //    try
        //    {
        //        result = (Results<Staff>)MakeRequest("staff?limit=-1", "GET", null, typeof(Results<Staff>)).Result;
        //        staff = result.data;
        //        foreach (var s in staff)
        //        {
        //            foreach (string f in s.worked_on)
        //            {
        //                if (f.Contains(gameId))
        //                {
        //                    staff1.Add(s);
        //                }
        //            }
        //        }
        //        //staff = staff.Where(x=>x.worked_on.Contains(gameId)).ToList();
        //    }
        //    catch { }
        //    return staff1;
        //}

        //public int getCountStaff(string gameId)
        //{
        //    int count = -1;
        //    try
        //    {
        //        count = GetStaff(gameId).Count();
        //    }
        //    catch { }
        //    return count;
        //}

        //public List<Character> GetCharacters(string gameId)
        //{
        //    List<Character> characters = null;
        //    List<Character> chars = new List<Character>();
        //    Results<Character> result = null;
        //    try
        //    {
        //        result = (Results<Character>)MakeRequest("characters?limit=-1", "GET", null, typeof(Results<Character>)).Result;
        //        characters = result.data;
        //        foreach (var s in characters)
        //        {
        //            foreach (string f in s.appearances)
        //            {
        //                if (f.Contains(gameId))
        //                {
        //                    chars.Add(s);
        //                }
        //            }
        //        }
        //        chars = chars.OrderBy(x => x.name).ToList();
        //        //staff = staff.Where(x=>x.worked_on.Contains(gameId)).ToList();
        //    }
        //    catch { }
        //    return chars;
        //}

        //public List<Place> getPlaces()
        //{
        //    List<Place> places = null;
        //    Results<Place> results = null;
        //    try
        //    {
        //        results = (Results<Place>)MakeRequest("places?limit=-1", "GET", null, typeof(Results<Place>)).Result;
        //        places = results.data;
        //        foreach (var s in places)
        //        {
        //            s.numAppearances = s.appearances.Count();
        //            s.numInhabitants = s.inhabitants.Count();
        //        }
        //        places = places.OrderBy(x => x.name).ToList();
        //    }
        //    catch { }
        //    if (places == null)
        //    {
        //        places = new List<Place>();
        //    }
        //    return places;
        //}

        //public List<Game> getAppareances(List<string> urls)
        //{
        //    List<Game> games = new List<Game>();
        //    Result<Game> result = new Result<Game>();
        //    foreach (var url in urls)
        //    {
        //        var pos = url.LastIndexOf("/");
        //        string gameid = url.Substring(pos, url.Length - pos);
        //        try
        //        {
        //            result = (Result<Game>)MakeRequest("games" + gameid, "GET", null, typeof(Result<Game>)).Result;
        //            games.Add(result.data);
        //        }
        //        catch { }
        //    }
        //    return games;
        //}

        //public List<Character> getInhabitants(List<string> urls)
        //{
        //    List<Character> character = new List<Character>();
        //    Result<Character> result = null;
        //    foreach (var url in urls)
        //    {
        //        var pos = url.LastIndexOf("/");
        //        string charId = url.Substring(pos, url.Length - pos);

        //        try
        //        {
        //            result = (Result<Character>)MakeRequest("characters" + charId, "GET", null, typeof(Result<Character>)).Result;
        //            result.data.checkGenderAndRace();
        //            character.Add(result.data);
        //        }
        //        catch { }
        //    }
        //    return character;
        //}


        public async Task<object> MakeRequest(string url, string method, object JSONcontent, Type responseType)
        ////  url: Url a partir de la base 
        ////  method: "GET"/"POST"/"PUT"/"DELETE"
        ////  JSONcontent: objecte que se li passa en el body 
        ////  responseType:  tipus d'objecte que torna el Web Service (=typeof(tipus))

        ////  contentType: "application/json" en els casos que el Web Service torni objectes
        {
            HttpResponseMessage response;

            if (method == "DELETE")
            {
                response = httpClient.DeleteAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    throw new Exception(ErrorMessage);
                }
            }
            else if (method == "GET")
            {
                response = httpClient.GetAsync(url).Result;
            }
            else if (method == "POST" || method == "PUT")
            {
                var objectJson = JsonConvert.SerializeObject(JSONcontent);
                var content = new StringContent(objectJson, Encoding.UTF8, contentType);


                if (method == "POST")
                {
                    response = httpClient.PostAsync(url, content).Result;
                }
                else
                {
                    response = httpClient.PutAsync(url, content).Result;
                }
            }
            else
            {
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(json, responseType);
                return result;
            }
            else
            {
                throw new Exception(ErrorMessage);
            }
        }
        
    }
}
