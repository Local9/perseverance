const STORAGE_KEY = 'theme';
export const DARK_PREFERENCE = '(prefers-color-scheme: dark)';

const THEMES = {
  DARK: 'dark',
  LIGHT: 'light',
};

const prefersDarkThemes = () => window.matchMedia(DARK_PREFERENCE).matches;

export const toggleTheme = () => {
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

export const applyTheme = () => {
  const preferredTheme = prefersDarkThemes() ? THEMES.DARK : THEMES.LIGHT;
  let currentTheme = localStorage.getItem(STORAGE_KEY) ?? preferredTheme;
  const html = document.querySelector('html');

  if (currentTheme === THEMES.DARK) {
    document.body.classList.remove(THEMES.LIGHT);
    document.body.classList.add(THEMES.DARK);
    html.dataset.theme = THEMES.DARK;
  } else {
    document.body.classList.remove(THEMES.DARK);
    document.body.classList.add(THEMES.LIGHT);
    html.dataset.theme = THEMES.LIGHT;
  }
};