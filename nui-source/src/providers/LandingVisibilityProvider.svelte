<script lang="ts">
  import { useNuiEvent } from '@utils/useNuiEvent';
  import { visibilityLanding } from '@store/stores';

  let isVisible: boolean;

  visibilityLanding.subscribe((visible) => {
    isVisible = visible;
  });

  useNuiEvent<boolean>('setLandingVisible', (visible) => {
    visibilityLanding.set(visible);
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
