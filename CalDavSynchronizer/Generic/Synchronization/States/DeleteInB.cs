// This file is Part of CalDavSynchronizer (http://outlookcaldavsynchronizer.sourceforge.net/)
// Copyright (c) 2015 Gerhard Zehetbauer 
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Reflection;
using CalDavSynchronizer.Generic.EntityRelationManagement;
using log4net;

namespace CalDavSynchronizer.Generic.Synchronization.States
{
  internal class DeleteInB<TAtypeEntityId, TAtypeEntityVersion, TAtypeEntity, TBtypeEntityId, TBtypeEntityVersion, TBtypeEntity>
      : StateWithKnownData<TAtypeEntityId, TAtypeEntityVersion, TAtypeEntity, TBtypeEntityId, TBtypeEntityVersion, TBtypeEntity>
  {
    // ReSharper disable once StaticFieldInGenericType
    private static readonly ILog s_logger = LogManager.GetLogger (MethodInfo.GetCurrentMethod ().DeclaringType);
    
    public DeleteInB (EntitySyncStateEnvironment<TAtypeEntityId, TAtypeEntityVersion, TAtypeEntity, TBtypeEntityId, TBtypeEntityVersion, TBtypeEntity> environment, IEntityRelationData<TAtypeEntityId, TAtypeEntityVersion, TBtypeEntityId, TBtypeEntityVersion> knownData)
        : base(environment, knownData)
    {
    }

    public override void AddRequiredEntitiesToLoad (Func<TAtypeEntityId, bool> a, Func<TBtypeEntityId, bool> b)
    {

    }

    public override IEntitySyncState<TAtypeEntityId, TAtypeEntityVersion, TAtypeEntity, TBtypeEntityId, TBtypeEntityVersion, TBtypeEntity> FetchRequiredEntities (IDictionary<TAtypeEntityId, TAtypeEntity> aEntities, IDictionary<TBtypeEntityId, TBtypeEntity> bEntites)
    {
      return this;
    }

    public override IEntitySyncState<TAtypeEntityId, TAtypeEntityVersion, TAtypeEntity, TBtypeEntityId, TBtypeEntityVersion, TBtypeEntity> Resolve ()
    {
      return this;
    }

    public override IEntitySyncState<TAtypeEntityId, TAtypeEntityVersion, TAtypeEntity, TBtypeEntityId, TBtypeEntityVersion, TBtypeEntity> PerformSyncActionNoThrow ()
    {
      try
      {
        var newA = _environment.BRepository.Delete (_knownData.BtypeId);
        return Discard ();
      }
      catch (Exception x)
      {
        LogException (x);
        return CreateDoNothing ();
      }
    }

    public override void AddNewRelationNoThrow (Action<IEntityRelationData<TAtypeEntityId, TAtypeEntityVersion, TBtypeEntityId, TBtypeEntityVersion>> addAction)
    {
      s_logger.Error ("This state should have been left via PerformSyncActionNoThrow!"); 
    }
  }
}