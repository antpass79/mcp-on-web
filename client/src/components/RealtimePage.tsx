import React from 'react'
import { useAppStore, type ImageParameters } from '../store/appStore'

interface SliderProps {
  label: string
  parameter: keyof ImageParameters
  value: number
}

const Slider: React.FC<SliderProps> = ({ label, parameter, value }) => {
  const { setImageParameter } = useAppStore()

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newValue = parseInt(e.target.value, 10)
    setImageParameter(parameter, newValue)
  }

  return (
    <div className="slider-container">
      <label className="slider-label">{label}</label>
      <div className="slider-wrapper">
        <input
          type="range"
          min="0"
          max="100"
          value={value}
          onChange={handleChange}
          className="slider"
        />
        <span className="slider-value">{value}</span>
      </div>
    </div>
  )
}

const RealtimePage: React.FC = () => {
  const { imageParameters } = useAppStore()

  return (
    <div>
      <h1 className="page-title">Realtime</h1>
      <div>
        <Slider 
          label="Gain" 
          parameter="gain" 
          value={imageParameters.gain} 
        />
        <Slider 
          label="Width" 
          parameter="width" 
          value={imageParameters.width} 
        />
        <Slider 
          label="Focus" 
          parameter="focus" 
          value={imageParameters.focus} 
        />
      </div>
    </div>
  )
}

export default RealtimePage