import React from 'react';

function BrandFilter() {
  return (
    <>
      <div className="brand-filter">
        <span className="title">Brand</span>
        <div className="list-brand">
          <button className="" style={{ fontSize: '1em', padding: '10px' }}>
            All Brand
          </button>
          <button className="">
            {' '}
            <img
              src="data:image/jpeg;base64,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"
              alt=""

            />
          </button>
          <button>
            {' '}
            <img
              src="https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/Xiaomi_logo.svg/480px-Xiaomi_logo.svg.png"
              alt=""
            />
          </button>
        </div>
      </div>
    </>
  );
}

export default BrandFilter;
