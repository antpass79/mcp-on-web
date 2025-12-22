import React from 'react'
import { useAppStore, type Page } from '../store/appStore'

const Sidebar: React.FC = () => {
  const { currentPage, setCurrentPage } = useAppStore()

  const navItems: { id: Page; label: string }[] = [
    { id: 'realtime', label: 'Realtime' },
    { id: 'configuration', label: 'Configuration' },
  ]

  return (
    <div className="sidebar">
      <h2>MCP Client</h2>
      <nav>
        {navItems.map((item) => (
          <button
            key={item.id}
            className={`nav-item ${currentPage === item.id ? 'active' : ''}`}
            onClick={() => setCurrentPage(item.id)}
          >
            {item.label}
          </button>
        ))}
      </nav>
    </div>
  )
}

export default Sidebar