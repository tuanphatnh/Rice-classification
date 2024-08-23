import React from 'react'

function DateFilter() {
  return (
    <>
    <div className='date-filter'>{
      "From " }
        <input className='start-date' type={'date'} />  {
      "To " }
        <input className='end-date' type={'date'} /> 
    </div>
    
    </>
  )
}

export default DateFilter
