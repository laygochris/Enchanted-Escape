// Platformer Game Kit // https://kybernetik.com.au/platformer // Copyright 2021-2024 Kybernetik //

using PlatformerGameKit.Characters;
using System;

namespace PlatformerGameKit.BehaviourTrees
{
    /// <summary>
    /// A <see cref="ConditionNode"/> which checks if the character is in their <see cref="Character.Idle"/> state.
    /// </summary>
    /// <remarks>
    /// <strong>Documentation:</strong>
    /// <see href="https://kybernetik.com.au/platformer/docs/characters/brains/behaviour/specific#conditions">
    /// Behaviour Tree Brains - Conditions</see>
    /// </remarks>
    /// https://kybernetik.com.au/platformer/api/PlatformerGameKit.BehaviourTrees/IsIdle
    /// 
    [Serializable]
    public class IsIdle : ConditionNode
    {
        /************************************************************************************************************************/

        /// <inheritdoc/>
        public override bool Condition
        {
            get
            {
                var character = Context<Character>.Current;
                return character.StateMachine.CurrentState == character.Idle;
            }
        }

        /************************************************************************************************************************/
    }
}
