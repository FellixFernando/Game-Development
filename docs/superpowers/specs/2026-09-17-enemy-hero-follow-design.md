# Enemy Hero Follow Design

## Goal

Make `EnemyBL` follow the Hero when the Hero enters the enemy's forward-facing
Box Collider trigger, then stop chasing, wait one second, return to the patrol
point it was pursuing, and resume the existing patrol loop.

## Detection and setup

- The enemy owns a Box Collider configured with **Is Trigger**.
- The Hero is identified with the `Hero` tag, not by object name.
- The Hero has a Collider and Rigidbody. `W4_Hero` already requires a Rigidbody.
- `OnTriggerEnter` starts chasing and `OnTriggerExit` ends chasing.
- The Box Collider's position, rotation, and size define the forward field of
  view; no additional raycast or distance check is required.

## Enemy behavior

`EnemyBL` uses four states:

1. **Patrol**: move between serialized patrol points as before.
2. **Chase**: move toward the tagged Hero while the Hero remains in the trigger.
3. **Return**: after a one-second delay following trigger exit, move to the
   patrol point that was active when the chase began.
4. **Resume**: once that point is reached, continue the normal patrol sequence.

If the Hero re-enters during the delay or return, the enemy immediately returns
to **Chase**. Patrol behavior remains unchanged when the Hero is not detected.

## Scope

Changes are limited to `EnemyBL.cs` and Unity Inspector setup. `Sc_move3.cs`
continues to move the Hero, and `Camera.cs` continues to follow the object
named `Player`; the Hero's tag is independent of its name.

## Validation

Verify in Unity that entering the enemy trigger causes following, exiting
causes a one-second pause, the enemy returns to the chase-start patrol point,
and patrol continues. Verify re-entry interrupts the return behavior.
