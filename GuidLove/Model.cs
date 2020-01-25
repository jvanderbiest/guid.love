using System;
using System.Collections.Generic;
using System.Linq;

namespace GuidLove
{
    public class Model
    {
        public Model()
        {
            Hypens = true;
            GuidItems = 1;
        }

        private bool _uppercase;
        public bool Uppercase
        {
            get => _uppercase;
            set
            {
                _uppercase = value;
                UpdateGuid(false);
            }
        }

        public bool Braces
        {
            get => _braces;
            set
            {
                _braces = value;

                if (_braces)
                {
                    _parantheses = false;
                    _hypens = true;
                }
                UpdateGuid(false);
            }
        }

        public bool Hypens
        {
            get => _hypens;
            set
            {
                _hypens = value;

                if (!_hypens)
                {
                    _braces = false;
                    _parantheses = false;
                }
                UpdateGuid(false);
            }
        }

        public bool Parantheses
        {
            get => _parantheses;
            set
            {
                _parantheses = value;

                if (_parantheses)
                {
                    _braces = false;
                    _hypens = true;
                }
                UpdateGuid(false);
            }
        }

        public int GuidItems
        {
            get => _guidItems;
            set
            {
                _guidItems = value;
                UpdateGuid(true);
            }
        }

        private bool _braces;
        private bool _hypens;
        private bool _parantheses;
        private int _guidItems;

        private string GetGuid(Guid theGuid)
        {
            string stringGuid;
            if (Hypens)
            {
                stringGuid = theGuid.ToString("D");
            }
            else
            {
                stringGuid = theGuid.ToString("N");
            }

            if (Braces)
            {
                stringGuid = theGuid.ToString("B");
            }

            if (Parantheses)
            {
                stringGuid = theGuid.ToString("P");
            }

            if (Uppercase)
            {
                stringGuid = stringGuid.ToUpperInvariant();
            }
            else
            {
                stringGuid = stringGuid.ToLowerInvariant();
            }

            return stringGuid;
        }

        public string CurrentGuid = Guid.NewGuid().ToString();

        public void UpdateGuid(bool generateNew)
        {
            List<string> transformedGuids = new List<string>();

            if (!generateNew)
            {
                List<Guid> guids = CurrentGuid.Split("\n").Select(x => Guid.Parse(x)).ToList();
                foreach (var guid in guids)
                {
                    transformedGuids.Add(GetGuid(guid));
                }
            }
            else
            {
                for (int i = 0; i < GuidItems; i++)
                {
                    transformedGuids.Add(GetGuid(Guid.NewGuid()));
                }
            }

            CurrentGuid = string.Join("\n", transformedGuids);
        }
    }
}