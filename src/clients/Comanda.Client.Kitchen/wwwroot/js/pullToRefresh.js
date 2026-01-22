// Pull-to-refresh JavaScript helper
window.initPullToRefresh = function (containerElement, dotNetRef) {
    if (!containerElement || !dotNetRef) return;
    
    let startY = 0;
    let isTracking = false;
    
    const isAtTop = () => {
        return containerElement.scrollTop <= 0;
    };
    
    containerElement.addEventListener('touchstart', (e) => {
        if (e.touches.length === 1) {
            startY = e.touches[0].clientY;
            isTracking = isAtTop();
            dotNetRef.invokeMethodAsync('OnTouchStart', startY, isTracking);
        }
    }, { passive: true });
    
    containerElement.addEventListener('touchmove', (e) => {
        if (!isTracking || e.touches.length !== 1) return;
        
        const currentY = e.touches[0].clientY;
        const delta = currentY - startY;
        
        // Only handle pull-down when at top
        if (delta > 0 && isAtTop()) {
            dotNetRef.invokeMethodAsync('OnTouchMove', currentY);
            
            // Prevent default scroll when pulling
            if (delta > 10) {
                e.preventDefault();
            }
        }
    }, { passive: false });
    
    containerElement.addEventListener('touchend', () => {
        if (isTracking) {
            dotNetRef.invokeMethodAsync('OnTouchEnd');
        }
        isTracking = false;
    }, { passive: true });
    
    containerElement.addEventListener('touchcancel', () => {
        isTracking = false;
    }, { passive: true });
};







