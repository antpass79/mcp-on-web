import React from 'react'
import { useAppStore } from '../store/appStore'

const PromptArea: React.FC = () => {
  const { promptText, setPromptText, sendPrompt } = useAppStore()

  const handleKeyPress = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      sendPrompt()
    }
  }

  return (
    <div className="prompt-area">
      <input
        type="text"
        className="prompt-input"
        placeholder="Enter your prompt here..."
        value={promptText}
        onChange={(e) => setPromptText(e.target.value)}
        onKeyPress={handleKeyPress}
      />
      <button 
        className="send-button" 
        onClick={sendPrompt}
        disabled={!promptText.trim()}
      >
        Send
      </button>
    </div>
  )
}

export default PromptArea