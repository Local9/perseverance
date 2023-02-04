<script lang="ts">
  import { useNuiEvent } from '@utils/useNuiEvent';
  import { fetchNui } from '@utils/fetchNui';
  import { onMount } from 'svelte';
  import { visibilityTablet } from '../store/stores';

  let isVisible: boolean;

  visibilityTablet.subscribe((visible) => {
    isVisible = visible;
  });

  useNuiEvent<boolean>('setTabletVisible', (visible) => {
    visibilityTablet.set(visible);
  });

  onMount(() => {
    const keyHandler = (e: KeyboardEvent) => {
      if (isVisible && ['Escape'].includes(e.code)) {
        fetchNui('hideTabletUI');
        visibilityTablet.set(false);
      }
    };

    window.addEventListener('keydown', keyHandler);

    return () => window.removeEventListener('keydown', keyHandler);
  });
</script>

<main>
  {#if isVisible}
    <slot />
  {/if}
</main>

<style>
  main {
    height: 100%;
    width: 100%;
  }
</style>
