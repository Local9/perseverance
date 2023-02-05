import { toast } from '@zerodevx/svelte-toast'

export const success = m => toast.push(m, {
  theme: {
    '--toastColor': 'mintcream',
    '--toastBackground': 'rgba(72,187,120,0.9)',
    '--toastBarBackground': '#2F855A'
  }
})

export const warning = m => toast.push(m, {   theme: {
    '--toastBackground': 'rgb(255, 255, 204)',
    '--toastColor': 'black',
    '--toastBarBackground': 'rgb(255, 235, 59)'
  }
})

export const failure = m => toast.push(m, {   theme: {
    '--toastBackground': '#AF0606',
    '--toastColor': 'white',
    '--toastBarBackground': 'rgb(244, 67, 54)'
  }
})