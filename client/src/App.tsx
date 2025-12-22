import React from 'react'
import { useAppStore } from './store/appStore'
import Sidebar from './components/Sidebar'
import PromptArea from './components/PromptArea'
import RealtimePage from './components/RealtimePage'
import ConfigurationPage from './components/ConfigurationPage'

const App: React.FC = () => {
  const { currentPage } = useAppStore()

  const renderPage = () => {
    switch (currentPage) {
      case 'realtime':
        return <RealtimePage />
      case 'configuration':
        return <ConfigurationPage />
      default:
        return <RealtimePage />
    }
  }

  return (
    <div className="app">
      <div className="main-content">
        <Sidebar />
        <div className="content-area">
          {renderPage()}
        </div>
      </div>
      <PromptArea />
    </div>
  )
}

export default App