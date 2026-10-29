# Aiming Laser Visibility Design

## Goal

Make the Week 7 laser visible only while the character is aiming.

## Design

The existing `hero` script owns the `isAim` Animator bool. The `laser` script
will read that bool from the character Animator and control its own
`LineRenderer`:

- When `isAim` is `true`, enable the line renderer and update its raycast.
- When `isAim` is `false`, disable the line renderer and skip raycasting.

The laser GameObject and character hierarchy do not need additional scene
references. The laser script will locate the Animator through its parent
hierarchy, matching the existing weapon/character setup.

## Error handling

If no Animator can be found, the script will log an explicit error and disable
itself rather than repeatedly throwing null-reference exceptions.

## Validation

The script must compile without diagnostics. In Unity Play Mode, holding the
right mouse button must show and update the laser, and releasing it must hide
the laser.
