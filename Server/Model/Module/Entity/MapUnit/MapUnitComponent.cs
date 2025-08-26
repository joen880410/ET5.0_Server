using System.Collections.Generic;
using System.Linq;
using ETHotfix;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ETModel
{
    public class MapUnitComponent : Component
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private readonly Dictionary<long, MapUnit> idUnits = new Dictionary<long, MapUnit>();

        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private readonly Dictionary<long, MapUnit> uidUnits = new Dictionary<long, MapUnit>();


        public List<MapUnit> playerMapunits => uidUnits.Values.Where(e => e.MapUnitType == MapUnitType.Hero).ToList();


        public void Destroy()
        {
            this.idUnits.Clear();
            this.uidUnits.Clear();
            base.Dispose();
        }

        public void Add(MapUnit unit)
        {
            if (this.uidUnits.TryAdd(unit.Uid, unit))
            {
                this.idUnits.Add(unit.Id, unit);
            }
        }

        public MapUnit Get(long id)
        {
            this.idUnits.TryGetValue(id, out MapUnit mapUnit);
            return mapUnit;
        }

        public MapUnit GetByUid(long uid)
        {
            this.uidUnits.TryGetValue(uid, out MapUnit mapUnit);
            return mapUnit;
        }
        public List<MapUnit> GetByUids(List<long> uids)
        {
            var mapUnits = new List<MapUnit>();
            for (int i = 0; i < uids.Count; i++)
            {
                var uid = uids[i];
                MapUnit mapUnit = GetByUid(uid);
                if (mapUnit != null)
                {
                    mapUnits.Add(mapUnit);
                }
            }
            return mapUnits;
        }

        public void RemoveUnit(MapUnit mapUnit)
        {
            this.idUnits.Remove(mapUnit.Id);
            this.uidUnits.Remove(mapUnit.Uid);
        }

        public int Count
        {
            get
            {
                return this.idUnits.Count;
            }
        }

        public MapUnit[] GetAll()
        {
            return this.idUnits.Values.ToArray();
        }
    }
}