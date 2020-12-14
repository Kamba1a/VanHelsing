using System;

namespace BeastHunter
{
    public sealed class AttackService : Service
    {
        #region Fields

        private Damage _damage;
        private GameContext _gameContext;

        #endregion


        #region ClassLifeCycles

        public AttackService(Contexts contexts) : base(contexts)
        {
            _damage = new Damage();
            _gameContext = contexts as GameContext;
        }

        #endregion


        #region Methods

        [ObsoleteAttribute("method to delete")]
        public Damage CountDamage(WeaponData weapon, BaseStatsClass dealerStats, BaseStatsClass recieverStats)
        {
            float weaponWeight = weapon is OneHandedWeaponData ? (weapon as OneHandedWeaponData).ActualWeapon.Weight :
                (weapon as TwoHandedWeaponData).CurrentAttack.WeaponItem.Weight;

            _damage.PhysicalDamage = weapon.CurrentAttack.AttackDamage.PhysicalDamage * weaponWeight *
                dealerStats.PhysicalPower * (1 - recieverStats.PhysicalResistance);
            _damage.StunProbability = weapon.CurrentAttack.AttackDamage.StunProbability - recieverStats.StunResistance > 0 ?
                weapon.CurrentAttack.AttackDamage.StunProbability - recieverStats.StunResistance : 0;

            return _damage;
        }
        /*
        [ObsoleteAttribute("method to delete")]
        public Damage CountDamage(Damage weaponDamage, BaseStatsClass dealerStats, BaseStatsClass recieverStats)
        {
            _damage.PhysicalDamage = weaponDamage.PhysicalDamage * dealerStats.PhysicalPower * 
                (1 - recieverStats.PhysicalResistance);
            _damage.StunProbability = weaponDamage.StunProbability - recieverStats.StunResistance > 0 ?
                weaponDamage.StunProbability - recieverStats.StunResistance : 0;

            return _damage;
        }
        */
        [ObsoleteAttribute("method to delete")]
        public Damage CountDamage(Damage weaponDamage, BaseStatsClass recieverStats)
        {
            _damage.PhysicalDamage = weaponDamage.PhysicalDamage * (1 - recieverStats.PhysicalResistance);
            _damage.StunProbability = weaponDamage.StunProbability * (1 - recieverStats.StunResistance);

            return _damage;
        }

        public Damage CountDamageToNpc(Damage weaponDamage, int receiverID)
        {
            BaseStatsClass receiverStats = _gameContext.NpcModels[receiverID].GetStats().MainStats;

            _damage.PhysicalDamage = weaponDamage.PhysicalDamage * (1 - receiverStats.PhysicalResistance);
            _damage.StunProbability = weaponDamage.StunProbability * (1 - receiverStats.StunResistance);

            return _damage;
        }

        public Damage CountDamageToNpc(Damage weaponDamage, BaseStatsClass dealerStats, int receiverID)
        {
            BaseStatsClass receiverStats = _gameContext.NpcModels[receiverID].GetStats().MainStats;
            _damage.PhysicalDamage = weaponDamage.PhysicalDamage * dealerStats.PhysicalPower *
                (1 - receiverStats.PhysicalResistance);
            _damage.StunProbability = weaponDamage.StunProbability - receiverStats.StunResistance > 0 ?
                weaponDamage.StunProbability - receiverStats.StunResistance : 0;

            return _damage;
        }

        public Damage CountDamageToPlayer(Damage weaponDamage)
        {
            BaseStatsClass receiverStats = _gameContext.CharacterModel.CharacterStats;

            _damage.PhysicalDamage = weaponDamage.PhysicalDamage * (1 - receiverStats.PhysicalResistance);
            _damage.StunProbability = weaponDamage.StunProbability * (1 - receiverStats.StunResistance);

            return _damage;
        }

        public Damage CountDamageToPlayer(Damage weaponDamage, BaseStatsClass dealerStats)
        {
            BaseStatsClass receiverStats = _gameContext.CharacterModel.CharacterStats;
            _damage.PhysicalDamage = weaponDamage.PhysicalDamage * dealerStats.PhysicalPower *
                (1 - receiverStats.PhysicalResistance);
            _damage.StunProbability = weaponDamage.StunProbability - receiverStats.StunResistance > 0 ?
                weaponDamage.StunProbability - receiverStats.StunResistance : 0;

            return _damage;
        }

        #endregion
    }
}

