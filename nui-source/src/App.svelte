<script lang="ts">
  import './global.scss';
  import Landing from '@pages/base/Landing.svelte';
  import Tablet from '@pages/tablet/Tablet.svelte';
  import { onMount } from 'svelte';
  import { SvelteToast } from '@zerodevx/svelte-toast';
  import TabletVisibilityProvider from '@providers/TabletVisibilityProvider.svelte';
  import LandingVisibilityProvider from '@providers/LandingVisibilityProvider.svelte';

  import { debugData } from '@utils/debugData';
  import { applyTheme, DARK_PREFERENCE } from '@store/theme';

  const options = {
    duration: 5000, // duration of progress bar tween to the `next` value
    initial: 1, // initial progress bar value
    next: 0, // next progress value
    pausable: false, // pause progress bar tween on mouse hover
    dismissable: false, // allow dismiss with close button
    reversed: false, // insert new toast to bottom of stack
    intro: { x: 256 }, // toast intro fly animation settings
    theme: {}, // css var overrides
    classes: [], // user-defined classes
  };

  onMount(() => {
    applyTheme();
    window.matchMedia(DARK_PREFERENCE).addEventListener('change', applyTheme);
  });

  debugData([
    {
      action: 'setLandingVisible',
      data: true,
    },
  ]);
</script>

<SvelteToast {options} />

<LandingVisibilityProvider>
  <Landing />
</LandingVisibilityProvider>

<TabletVisibilityProvider>
  <Tablet />
</TabletVisibilityProvider>
