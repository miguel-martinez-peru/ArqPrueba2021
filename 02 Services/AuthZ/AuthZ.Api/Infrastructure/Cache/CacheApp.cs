using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthZ.Api.Infrastructure.Cache
{
    /// <summary>
    /// Cache del aplicacion
    /// </summary>
    public static class CacheApp
    {

        #region VARIABLES

        /// <summary>
        /// Objeto principal que contiene toda la cache
        /// </summary>
        private readonly static MemoryCache _cache;

        #endregion

        #region CONSTRUCTORES

        /// <summary>
        /// Contructor principal
        /// </summary>
        static CacheApp()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        //public static List<T> ResolverLista<T>(string v1, List<T> result, string v2, string v3, bool v4)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region MÉTODOS - Implementacion ICache

        /// <summary>
        /// Implementa método para obtener la lista, sin agregar un primer item, guardar en cache y retorna los datos.
        /// </summary>
        /// <typeparam name="TTypeResult"></typeparam>
        /// <param name="name">Nombre del parametro</param>
        /// <param name="methodCall">Metodo a llamar para obtener datos, si estos no existen en cache</param>
        /// <param name="minutes">Tiempo que los datos permanecen en cache, 0 = sin fecha de expiración.</param>
        /// <returns></returns>
        public static List<TTypeResult> ResolverLista<TTypeResult>(
            string name, Func<IEnumerable<TTypeResult>> methodCall,
            string orden, string ordenDir, bool refresh = true, Double minutes = 0)
            where TTypeResult : class
        {
            if (!refresh)
            {
                List<TTypeResult> listResult;
                if (_cache.TryGetValue(name, out listResult))
                    return listResult;

                List<TTypeResult> listDataSource = methodCall.Invoke().ToList();
                if (listDataSource.Any())
                {
                    AddItem(name, listDataSource, minutes);
                    return listDataSource;
                }
                return listDataSource;
            }
            else
            {
                var listDataSource = methodCall.Invoke().ToList();
                if (listDataSource.Any())
                {
                    RemoveItem(name);
                    AddItem(name, listDataSource, minutes);

                    return listDataSource;
                }
                return listDataSource;
            }
        }

        #endregion

        #region MÉTODOS - Apoyo

        /// <summary>
        /// Agrega un item en cache
        /// </summary>
        /// <typeparam name="T">Tipo a agregar</typeparam>
        /// <param name="name">Nombre del parametro</param>
        /// <param name="value">Objeto a agregar</param>
        /// <param name="minutes">Tiempo que los datos permanecen en cache, 0 = sin fecha de expiración.</param>
        private static void AddItem<T>(string name, T value, Double minutes) where T : class
        {
            if (value != null)
            {
                if (minutes > 0)
                {
                    _cache.Set(name, value, new TimeSpan(0, (int)minutes, 0));
                }
                else
                {
                    var cacheItemPolicy = new MemoryCacheEntryOptions().SetAbsoluteExpiration(new TimeSpan(0, 10, 0));
                    _cache.Set(name, value, cacheItemPolicy);
                }
            }
        }

        /// <summary>
        /// RemoveItem
        /// </summary>
        /// <param name="name">name</param>
        private static void RemoveItem(string name)
        {
            object cacheEntry;
            if (_cache.TryGetValue(name, out cacheEntry))
                _cache.Remove(name);
        }

        #endregion

    }
}
