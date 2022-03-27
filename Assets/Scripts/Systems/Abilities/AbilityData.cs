using System.Collections.Generic;
using Ships.Components;
using Ships.Fleets;
using Systems.Modifiers;
using UI.InfoWindow;
using UnityEngine;

namespace Systems.Abilities
{
    /// <summary>
    ///     Data for an ability that can be activated to apply modifiers to a target, with a set cooldown
    ///     between activations.
    /// </summary>
    [CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability", order = 0)]
    public class AbilityData : UniqueId
    {
        [Header("Info")] [SerializeField] private GameObject mapIcon;
        [SerializeField] private Sprite infoWindowSprite;
        [SerializeField] private string attackName;
        [SerializeField] private int cost;
        [SerializeField] private List<ModifierData> modifiers;
        [SerializeField] private float cooldown;
        [SerializeField] private ButtonData buttonData;
        [SerializeField] private FleetAgroStatus validTargets;
        [SerializeField] private List<string> tags;
        [SerializeField] private string fireSound;
        [SerializeField] private string hitSound;

        [Header("Projectile Stats")] [SerializeField]
        private float hitChance;

        [SerializeField] private float moduleHitChance;
        [SerializeField] private int damage;
        [SerializeField] private float moduleDamagePercent;
        [SerializeField] private float mapSpeed;
        [SerializeField] private float infoWindowSpeed;
        [SerializeField] private float range;

        [Header("InfoWindow Animations")] [SerializeField]
        private GameObject hitAnimation;

        [SerializeField] private GameObject missAnimation;
        [SerializeField] private GameObject fireAnimation;
        
        public List<ModifierData> Modifiers => modifiers;
        public float Cooldown => cooldown;
        public ButtonData ButtonData => buttonData;
        public FleetAgroStatus ValidTargets => validTargets;
        public List<string> Tags => tags;
        public string FireSound => fireSound;
        public string HitSound => hitSound;
        public GameObject MapIcon => mapIcon;
        public Sprite InfoWindowSprite => infoWindowSprite;
        public string AttackName => attackName;
        public int Cost => cost;
        public float BaseHitChance => hitChance;
        public float BaseModuleHitChance => moduleHitChance;
        public float BaseDamage => damage;
        public float BaseRange => range;
        public float ModuleDamagePercent => moduleDamagePercent;
        public float BaseMapSpeed => mapSpeed;
        public float BaseInfoWindowSpeed => infoWindowSpeed;
        public GameObject HitAnimation => hitAnimation;
        public GameObject MissAnimation => missAnimation;
        public GameObject FireAnimation => fireAnimation;

        public Ability MakeAbility(ShipStats shipStats, DamageableComponent damageableComponent = null)
        {
            return new Ability(this, shipStats, damageableComponent);
        }
    }
}