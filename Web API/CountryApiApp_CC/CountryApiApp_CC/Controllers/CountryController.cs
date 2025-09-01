using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CountryApiApp_CC.Models;

public class CountryController : ApiController
{
    // Static in-memory list
    private static List<Country> countries = new List<Country>
    {
        new Country { ID = 1, CountryName = "India", Capital = "New Delhi" },
        new Country { ID = 2, CountryName = "France", Capital = "Paris" }
    };

    // GET: api/Country
    public IEnumerable<Country> Get()
    {
        return countries;
    }

    // GET: api/Country/1
    public IHttpActionResult Get(int id)
    {
        var country = countries.FirstOrDefault(x => x.ID == id);
        if (country == null)
            return NotFound();
        return Ok(country);
    }

    // POST: api/Country
    public IHttpActionResult Post(Country country)
    {
        country.ID = countries.Count > 0 ? countries.Max(x => x.ID) + 1 : 1;
        countries.Add(country);
        return CreatedAtRoute("DefaultApi", new { id = country.ID }, country);
    }

    // PUT: api/Country/1
    public IHttpActionResult Put(int id, Country country)
    {
        var existing = countries.FirstOrDefault(x => x.ID == id);
        if (existing == null)
            return NotFound();
        existing.CountryName = country.CountryName;
        existing.Capital = country.Capital;
        return Ok(existing);
    }

    // DELETE: api/Country/1
    public IHttpActionResult Delete(int id)
    {
        var country = countries.FirstOrDefault(x => x.ID == id);
        if (country == null)
            return NotFound();
        countries.Remove(country);
        return Ok();
    }
}
