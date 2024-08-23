import { useEffect, useState } from 'react';
import classNames from 'classnames/bind';
import styles from './Slider.module.scss';

import slide1 from '~/assets/images/slide-2.jpg';
import slide2 from '~/assets/images/slide-4.jpg';
import slide3 from '~/assets/images/slide-6.jpg';
import slide4 from '~/assets/images/slide-5.jpg';

const cx = classNames.bind(styles);

function Slider() {
    const slides = [slide1, slide2, slide3, slide4];
    const [activeIndex, setActiveIndex] = useState(0);

    useEffect(() => {
        const interval = setInterval(() => {
            setActiveIndex((prevIndex) => (prevIndex + 1) % slides.length);
        }, 5000);

        return () => {
            clearInterval(interval);
        };
    }, [slides.length]);

    const handleDotClick = (index) => {
        setActiveIndex(index);
    };

    return (
        <div className={cx('wrap')}>
            {slides.map((slide, index) => (
                <img
                    key={index}
                    className={cx('img', { active: index === activeIndex })}
                    src={slide}
                    alt=""
                />
            ))}
            <div className={cx('list')}>
                {slides.map((_, index) => (
                    <div
                        key={index}
                        className={cx('item', { active: index === activeIndex })}
                        onClick={() => handleDotClick(index)}
                    ></div>
                ))}
            </div>
        </div>
    );
}

export default Slider;
