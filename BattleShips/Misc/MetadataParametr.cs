// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Metadata;

namespace BattleShips.Misc
{
    public class MetadataParametr
    {
        private IMetadata _gameMetadate;

        private IMetadata _playerMetadate;

        public MetadataParametr(GameMetadata metadata) => _gameMetadate = metadata;

        public MetadataParametr(PlayerMetadate playerMetadate) => _playerMetadate = playerMetadate;

        public IMetadata GetMetadata()
        {
            return _gameMetadate ?? _playerMetadate;
        }
    }
}