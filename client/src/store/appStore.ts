import { create } from 'zustand'

export type Page = 'realtime' | 'configuration'

export interface ImageParameters {
  gain: number
  width: number
  focus: number
}

export interface AppState {
  currentPage: Page
  imageParameters: ImageParameters
  promptText: string
  setCurrentPage: (page: Page) => void
  setImageParameter: (param: keyof ImageParameters, value: number) => void
  setPromptText: (text: string) => void
  sendPrompt: () => void
}

export const useAppStore = create<AppState>((set, get) => ({
  currentPage: 'realtime',
  imageParameters: {
    gain: 50,
    width: 50,
    focus: 50,
  },
  promptText: '',
  
  setCurrentPage: (page: Page) => set({ currentPage: page }),
  
  setImageParameter: (param: keyof ImageParameters, value: number) =>
    set((state) => ({
      imageParameters: {
        ...state.imageParameters,
        [param]: value,
      },
    })),
  
  setPromptText: (text: string) => set({ promptText: text }),
  
  sendPrompt: () => {
    const { promptText } = get()
    if (promptText.trim()) {
      // Mock implementation - just log for now
      console.log('Sending prompt:', promptText)
      // Clear the prompt after sending
      set({ promptText: '' })
    }
  },
}))