// Function to get room ID from URL
function getRoomIdFromUrl() {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get('id');
}

// Function to generate rating stars HTML
function generateRatingStars(rating) {
    let starsHtml = '';
    for (let i = 1; i <= 5; i++) {
        if (i <= rating) {
            starsHtml += '<span class="fa fa-star checked"></span>';
        } else {
            starsHtml += '<span class="fa fa-star"></span>';
        }
    }
    return starsHtml;
}

// Function to fetch room details from API
async function fetchRoomDetails() {
    try {
        const roomId = getRoomIdFromUrl();
        if (!roomId) {
            console.error('No room ID found in URL');
            return;
        }

        const response = await fetch(`http://localhost:5178/api/RoomType/${roomId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const result = await response.json();
        if (result.success && result.data) {
            displayRoomDetails(result.data);
        } else {
            console.error('Failed to fetch room details:', result.message);
        }
    } catch (error) {
        console.error('Error fetching room details:', error);
    }
}

// Function to display room details
function displayRoomDetails(room) {
    // Update breadcrumb
    $('.breadcrumb-item.active').text(room.name);

    // Update room title and rating
    $('.detail-title .title-left h3').text(room.name);
    $('.detail-title .title-left .rating').html(generateRatingStars(room.rating));
    
    // Update price
    $('.detail-title .title-right .title-price h3').html(`$${room.pricePerNight}<span>/Night</span>`);

    // Update room images
    const imageGallery = $('.detail-slider');
    imageGallery.empty();
    
    room.imgs.forEach(img => {
        const slideHtml = `
            <div class="detail-slide">
                <img src="${img}" alt="${room.name}" />
            </div>
        `;
        imageGallery.append(slideHtml);
    });

    // Update room description
    $('.detail-content .detail-desc p').text(room.description);

    // Update room features
    const featuresHtml = `
        <div class="row">
            <div class="col-md-6 col-sm-6 col-xs-6">
                <i class="fa fa-bed" aria-hidden="true"></i> Max Occupancy: ${room.maxOccupancy}
            </div>
            <div class="col-md-6 col-sm-6 col-xs-6">
                <i class="fa fa-child" aria-hidden="true"></i> Children Allowed: ${room.childrenAllowed}
            </div>
            <div class="col-md-6 col-sm-6 col-xs-6">
                <i class="fa fa-hotel" aria-hidden="true"></i> Room Code: ${room.code}
            </div>
            <div class="col-md-6 col-sm-6 col-xs-6">
                <i class="fa fa-door-open" aria-hidden="true"></i> Available Rooms: ${room.emptyRooms}
            </div>
        </div>
    `;
    $('.detail-content .room-services').html(featuresHtml);

    // Initialize slider after updating content
    if (typeof $.fn.slick !== 'undefined') {
        $('.detail-slider').slick({
            dots: true,
            infinite: true,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 3000
        });
    }
}

// Function to handle booking button click
function handleBookingClick() {
    const roomId = getRoomIdFromUrl();
    if (roomId) {
        // Store room ID in localStorage for booking process
        localStorage.setItem('selectedRoomId', roomId);
        // Redirect to booking page
        window.location.href = 'booking.html';
    }
}

// Initialize page
$(document).ready(function() {
    // Fetch and display room details
    fetchRoomDetails();

    // Add click handler for booking button
    $('.btn-orange').on('click', function(e) {
        e.preventDefault();
        handleBookingClick();
    });
}); 