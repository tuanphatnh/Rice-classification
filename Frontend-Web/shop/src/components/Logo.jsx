import React from 'react'

function Logo({expand}) {
  return (
    <div className='logo'>
      
      <img alt='logo' src='https://as1.ftcdn.net/v2/jpg/01/66/92/98/500_F_166929804_gDX10vuJpVMiqGxozHNLM1nsH43mAU6g.jpg' />
      {expand ? "" : <p>Thoi so</p>}
    </div>
  )
}

export default Logo
