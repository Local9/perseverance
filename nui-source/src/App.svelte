<script lang="ts">
  import './global.scss';
  import Landing from '@pages/base/Landing.svelte';
  import Tablet from '@pages/tablet/Tablet.svelte';
  import { onMount } from 'svelte';
  import { SvelteToast } from '@zerodevx/svelte-toast';
  import TabletVisibilityProvider from '@providers/TabletVisibilityProvider.svelte';
  import LandingVisibilityProvider from '@providers/LandingVisibilityProvider.svelte';

  import { debugData } from '@utils/debugData';

  const STORAGE_KEY = 'theme';
  const DARK_PREFERENCE = '(prefers-color-scheme: dark)';

  const THEMES = {
    DARK: 'dark',
    LIGHT: 'light',
  };

  const prefersDarkThemes = () => window.matchMedia(DARK_PREFERENCE).matches;

  const toggleTheme = () => {
    const stored = localStorage.getItem(STORAGE_KEY);

    if (stored) {
      // clear storage
      localStorage.removeItem(STORAGE_KEY);
    } else {
      // store opposite of preference
      localStorage.setItem(
        STORAGE_KEY,
        prefersDarkThemes() ? THEMES.LIGHT : THEMES.DARK
      );
    }
    applyTheme();
  };

  const applyTheme = () => {
    const preferredTheme = prefersDarkThemes() ? THEMES.DARK : THEMES.LIGHT;
    let currentTheme = localStorage.getItem(STORAGE_KEY) ?? preferredTheme;

    if (currentTheme === THEMES.DARK) {
      document.body.classList.remove(THEMES.LIGHT);
      document.body.classList.add(THEMES.DARK);
    } else {
      document.body.classList.remove(THEMES.DARK);
      document.body.classList.add(THEMES.LIGHT);
    }
  };

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
