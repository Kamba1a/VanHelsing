namespace BeastHunter
{
    public sealed class MainControllers : ControllersStart
    {
        #region ClassLifeCycles

        public MainControllers(GameContext context)
        {
            AddInitializeControllers(context);
            AddControllers(context);

            //Add(new QuestIndicatorInitializeController(context));
            //Add(new QuestJournalInitializeController(context));
            //Add(new QuestIndicatorController(context));
            //Add(new QuestIndicatorController(context));
        }

        #endregion


        #region Methods

        private void AddInitializeControllers(GameContext context) 
        {
            Add(new UIBestiarylInitializeController(context));
            //Add<T>(context) where T : NpcInitializeController

            Add(new CharacterInitializeController(context));
            //Add(new BossInitializeController(context));

            

            //Add(new SmthInitializeController(context));

            //Add(new BossInitializeController(context));
            //Add(new GiantMudCrabInitilizeController(context));
            //Add(new RabbitInitializeController(context));
            //Add(new DialogueSystemInitializeController(context));
            //Add(new StartDialogueInitializeController(context));
            //Add(new QuestInitializeController(context));
            //Add(new HellHoundInitializeController(context));
            Add(new BouldersInitializeController(context));
        }

        private void AddControllers(GameContext context)
        {
            Add(new EnemyController(context));
            //Add(new GiantMudCrabController(context));
            //Add(new TargetController(context));
            Add(new InputController(context));
            Add(new TimeRemainingController(context));
            Add(new CharacterController(context));
            Add(new TrapController(context));

            

            Add(new InteractiveObjectController(context));

            //Add(new DialogueSystemController(context));
            //Add(new StartDialogueController(context));
            //Add(new DialogueTriggerController(context));
            //Add(new QuestController(context));
            Add(new UIBestiaryController(context));
        }

        #endregion
    }
}
