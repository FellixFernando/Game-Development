# Aiming Laser Visibility Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Show the Week 7 laser only while the character's `isAim` Animator bool is true.

**Architecture:** `laser.cs` will find the character Animator through its parent hierarchy, cache the existing `LineRenderer`, and enable or disable that renderer from the Animator's `isAim` bool. Raycast and endpoint updates will run only while aiming. Missing required components will produce explicit errors and disable the script.

**Tech Stack:** Unity C#, `Animator`, `LineRenderer`, `Physics.Raycast`.

## Global Constraints

- Preserve the existing right-mouse aiming flow in `hero.cs`.
- Do not add scene references or new dependencies.
- Keep the laser raycast behavior unchanged while aiming.
- Hide the laser and skip raycasting when `isAim` is false.
- Report missing required components explicitly instead of silently continuing.

---

### Task 1: Gate laser rendering by aiming state

**Files:**
- Modify: `Assets/Scripts/Week7/laser.cs`
- Test: Unity C# diagnostics and Week 7 Play Mode

**Interfaces:**
- Consumes: The parent hierarchy's `Animator` and its existing `isAim` bool parameter.
- Produces: A self-contained laser component that enables its `LineRenderer` only while aiming.

- [ ] **Step 1: Cache and validate required components**

  In `Start()`, cache the local `LineRenderer` and locate the parent `Animator` with `GetComponentInParent<Animator>()`. If either is missing, log an error with `this` as the context, disable the script, and return.

- [ ] **Step 2: Initialize the laser hidden**

  After validation, set `lr.enabled = false` so the laser cannot flash before the first aim-state check.

- [ ] **Step 3: Update visibility before raycasting**

  At the beginning of `Update()`, read `anim.GetBool("isAim")`, assign that value to `lr.enabled`, and return immediately when false.

- [ ] **Step 4: Preserve the existing aiming raycast**

  Leave the current `SetPosition` and `Physics.Raycast` logic unchanged for the aiming path so the visible laser continues to use the current transform, forward direction, hit point, and 100-unit fallback.

- [ ] **Step 5: Verify the script**

  Run the editor diagnostics for `Assets/Scripts/Week7/laser.cs`.

  Expected: no C# errors or warnings for the edited file.

- [ ] **Step 6: Verify behavior in Unity**

  Open `Scenes/Week7.unity` and enter Play Mode. Hold the right mouse button and confirm the line appears and follows the raycast. Release the button and confirm the line disappears and no longer updates.

- [ ] **Step 7: Commit the focused change**

  From the repository root, run:

  ```bash
  git add Assets/Scripts/Week7/laser.cs docs/superpowers/specs/2026-10-08-aiming-laser-visibility-design.md docs/superpowers/plans/2026-10-08-aiming-laser-visibility.md
  git commit -m "feat: show laser while aiming"
  ```

