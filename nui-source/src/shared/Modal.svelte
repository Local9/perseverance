<script lang="ts">
  import { fade, fly } from 'svelte/transition';
  import { cubicInOut, cubicOut } from 'svelte/easing';
</script>

<div class="modal-wrapper absolute w-full h-full z-10 flex items-center justify-center" transition:fade|local={{ duration: 500 }}>
  <div class="modal" in:fly|local={{ duration: 750, easing: cubicOut, y: -300 }} out:fly|local={{ duration: 400, easing: cubicInOut }}>
    <slot />
  </div>
</div>

<style lang="scss">
  .modal-wrapper {
    position: absolute;
    top: 0;
    left: 0;
    z-index: 10;
    height: 100%;
    width: 100%;
  }

  .modal {
    max-height: 80vh;
    height: auto;
    max-width: 1000px; // Will want to make this configurable
    width: 85vw; // Will want to make this configurable
    padding: 25px;
    background-color: rgba(var(--background-color-modal), 1);
    border-radius: calc(var(--tablet-height-ratio) / 48);
    padding: 25px;
    display: flex;
    flex-direction: column;
    overflow-x: auto;
    box-shadow: 2px 2px 2px rgba(0, 0, 0, var(--shadow-opacity-modal)), 0 0 0 1000px rgba(var(--background-color-modal), 0.75);
  }
</style>
