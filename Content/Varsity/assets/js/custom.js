//slick.js

/*
     _ _      _       _
 ___| (_) ___| | __  (_)___
/ __| | |/ __| |/ /  | / __|
\__ \ | | (__|   < _ | \__ \
|___/_|_|\___|_|\_(_)/ |___/
                   |__/

 Version: 1.5.9
  Author: Ken Wheeler
 Website: http://kenwheeler.github.io
    Docs: http://kenwheeler.github.io/slick
    Repo: http://github.com/kenwheeler/slick
  Issues: http://github.com/kenwheeler/slick/issues

 */
/* global window, document, define, jQuery, setInterval, clearInterval */
(function (factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        define(['jquery'], factory);
    } else if (typeof exports !== 'undefined') {
        module.exports = factory(require('jquery'));
    } else {
        factory(jQuery);
    }

}(function ($) {
    'use strict';
    var Slick = window.Slick || {};

    Slick = (function () {

        var instanceUid = 0;

        function Slick(element, settings) {

            var _ = this, dataSettings;

            _.defaults = {
                accessibility: true,
                adaptiveHeight: false,
                appendArrows: $(element),
                appendDots: $(element),
                arrows: true,
                asNavFor: null,
                prevArrow: '<button type="button" data-role="none" class="slick-prev" aria-label="Previous" tabindex="0" role="button">Previous</button>',
                nextArrow: '<button type="button" data-role="none" class="slick-next" aria-label="Next" tabindex="0" role="button">Next</button>',
                autoplay: false,
                autoplaySpeed: 3000,
                centerMode: false,
                centerPadding: '50px',
                cssEase: 'ease',
                customPaging: function (slider, i) {
                    return '<button type="button" data-role="none" role="button" aria-required="false" tabindex="0">' + (i + 1) + '</button>';
                },
                dots: false,
                dotsClass: 'slick-dots',
                draggable: true,
                easing: 'linear',
                edgeFriction: 0.35,
                fade: false,
                focusOnSelect: false,
                infinite: true,
                initialSlide: 0,
                lazyLoad: 'ondemand',
                mobileFirst: false,
                pauseOnHover: true,
                pauseOnDotsHover: false,
                respondTo: 'window',
                responsive: null,
                rows: 1,
                rtl: false,
                slide: '',
                slidesPerRow: 1,
                slidesToShow: 1,
                slidesToScroll: 1,
                speed: 500,
                swipe: true,
                swipeToSlide: false,
                touchMove: true,
                touchThreshold: 5,
                useCSS: true,
                useTransform: false,
                variableWidth: false,
                vertical: false,
                verticalSwiping: false,
                waitForAnimate: true,
                zIndex: 1000
            };

            _.initials = {
                animating: false,
                dragging: false,
                autoPlayTimer: null,
                currentDirection: 0,
                currentLeft: null,
                currentSlide: 0,
                direction: 1,
                $dots: null,
                listWidth: null,
                listHeight: null,
                loadIndex: 0,
                $nextArrow: null,
                $prevArrow: null,
                slideCount: null,
                slideWidth: null,
                $slideTrack: null,
                $slides: null,
                sliding: false,
                slideOffset: 0,
                swipeLeft: null,
                $list: null,
                touchObject: {},
                transformsEnabled: false,
                unslicked: false
            };

            $.extend(_, _.initials);

            _.activeBreakpoint = null;
            _.animType = null;
            _.animProp = null;
            _.breakpoints = [];
            _.breakpointSettings = [];
            _.cssTransitions = false;
            _.hidden = 'hidden';
            _.paused = false;
            _.positionProp = null;
            _.respondTo = null;
            _.rowCount = 1;
            _.shouldClick = true;
            _.$slider = $(element);
            _.$slidesCache = null;
            _.transformType = null;
            _.transitionType = null;
            _.visibilityChange = 'visibilitychange';
            _.windowWidth = 0;
            _.windowTimer = null;

            dataSettings = $(element).data('slick') || {};

            _.options = $.extend({}, _.defaults, dataSettings, settings);

            _.currentSlide = _.options.initialSlide;

            _.originalSettings = _.options;

            if (typeof document.mozHidden !== 'undefined') {
                _.hidden = 'mozHidden';
                _.visibilityChange = 'mozvisibilitychange';
            } else if (typeof document.webkitHidden !== 'undefined') {
                _.hidden = 'webkitHidden';
                _.visibilityChange = 'webkitvisibilitychange';
            }

            _.autoPlay = $.proxy(_.autoPlay, _);
            _.autoPlayClear = $.proxy(_.autoPlayClear, _);
            _.changeSlide = $.proxy(_.changeSlide, _);
            _.clickHandler = $.proxy(_.clickHandler, _);
            _.selectHandler = $.proxy(_.selectHandler, _);
            _.setPosition = $.proxy(_.setPosition, _);
            _.swipeHandler = $.proxy(_.swipeHandler, _);
            _.dragHandler = $.proxy(_.dragHandler, _);
            _.keyHandler = $.proxy(_.keyHandler, _);
            _.autoPlayIterator = $.proxy(_.autoPlayIterator, _);

            _.instanceUid = instanceUid++;

            // A simple way to check for HTML strings
            // Strict HTML recognition (must start with <)
            // Extracted from jQuery v1.11 source
            _.htmlExpr = /^(?:\s*(<[\w\W]+>)[^>]*)$/;


            _.registerBreakpoints();
            _.init(true);
            _.checkResponsive(true);

        }

        return Slick;

    }());

    Slick.prototype.addSlide = Slick.prototype.slickAdd = function (markup, index, addBefore) {

        var _ = this;

        if (typeof (index) === 'boolean') {
            addBefore = index;
            index = null;
        } else if (index < 0 || (index >= _.slideCount)) {
            return false;
        }

        _.unload();

        if (typeof (index) === 'number') {
            if (index === 0 && _.$slides.length === 0) {
                $(markup).appendTo(_.$slideTrack);
            } else if (addBefore) {
                $(markup).insertBefore(_.$slides.eq(index));
            } else {
                $(markup).insertAfter(_.$slides.eq(index));
            }
        } else {
            if (addBefore === true) {
                $(markup).prependTo(_.$slideTrack);
            } else {
                $(markup).appendTo(_.$slideTrack);
            }
        }

        _.$slides = _.$slideTrack.children(this.options.slide);

        _.$slideTrack.children(this.options.slide).detach();

        _.$slideTrack.append(_.$slides);

        _.$slides.each(function (index, element) {
            $(element).attr('data-slick-index', index);
        });

        _.$slidesCache = _.$slides;

        _.reinit();

    };

    Slick.prototype.animateHeight = function () {
        var _ = this;
        if (_.options.slidesToShow === 1 && _.options.adaptiveHeight === true && _.options.vertical === false) {
            var targetHeight = _.$slides.eq(_.currentSlide).outerHeight(true);
            _.$list.animate({
                height: targetHeight
            }, _.options.speed);
        }
    };

    Slick.prototype.animateSlide = function (targetLeft, callback) {

        var animProps = {},
            _ = this;

        _.animateHeight();

        if (_.options.rtl === true && _.options.vertical === false) {
            targetLeft = -targetLeft;
        }
        if (_.transformsEnabled === false) {
            if (_.options.vertical === false) {
                _.$slideTrack.animate({
                    left: targetLeft
                }, _.options.speed, _.options.easing, callback);
            } else {
                _.$slideTrack.animate({
                    top: targetLeft
                }, _.options.speed, _.options.easing, callback);
            }

        } else {

            if (_.cssTransitions === false) {
                if (_.options.rtl === true) {
                    _.currentLeft = -(_.currentLeft);
                }
                $({
                    animStart: _.currentLeft
                }).animate({
                    animStart: targetLeft
                }, {
                        duration: _.options.speed,
                        easing: _.options.easing,
                        step: function (now) {
                            now = Math.ceil(now);
                            if (_.options.vertical === false) {
                                animProps[_.animType] = 'translate(' +
                                    now + 'px, 0px)';
                                _.$slideTrack.css(animProps);
                            } else {
                                animProps[_.animType] = 'translate(0px,' +
                                    now + 'px)';
                                _.$slideTrack.css(animProps);
                            }
                        },
                        complete: function () {
                            if (callback) {
                                callback.call();
                            }
                        }
                    });

            } else {

                _.applyTransition();
                targetLeft = Math.ceil(targetLeft);

                if (_.options.vertical === false) {
                    animProps[_.animType] = 'translate3d(' + targetLeft + 'px, 0px, 0px)';
                } else {
                    animProps[_.animType] = 'translate3d(0px,' + targetLeft + 'px, 0px)';
                }
                _.$slideTrack.css(animProps);

                if (callback) {
                    setTimeout(function () {

                        _.disableTransition();

                        callback.call();
                    }, _.options.speed);
                }

            }

        }

    };

    Slick.prototype.asNavFor = function (index) {

        var _ = this,
            asNavFor = _.options.asNavFor;

        if (asNavFor && asNavFor !== null) {
            asNavFor = $(asNavFor).not(_.$slider);
        }

        if (asNavFor !== null && typeof asNavFor === 'object') {
            asNavFor.each(function () {
                var target = $(this).slick('getSlick');
                if (!target.unslicked) {
                    target.slideHandler(index, true);
                }
            });
        }

    };

    Slick.prototype.applyTransition = function (slide) {

        var _ = this,
            transition = {};

        if (_.options.fade === false) {
            transition[_.transitionType] = _.transformType + ' ' + _.options.speed + 'ms ' + _.options.cssEase;
        } else {
            transition[_.transitionType] = 'opacity ' + _.options.speed + 'ms ' + _.options.cssEase;
        }

        if (_.options.fade === false) {
            _.$slideTrack.css(transition);
        } else {
            _.$slides.eq(slide).css(transition);
        }

    };

    Slick.prototype.autoPlay = function () {

        var _ = this;

        if (_.autoPlayTimer) {
            clearInterval(_.autoPlayTimer);
        }

        if (_.slideCount > _.options.slidesToShow && _.paused !== true) {
            _.autoPlayTimer = setInterval(_.autoPlayIterator,
                _.options.autoplaySpeed);
        }

    };

    Slick.prototype.autoPlayClear = function () {

        var _ = this;
        if (_.autoPlayTimer) {
            clearInterval(_.autoPlayTimer);
        }

    };

    Slick.prototype.autoPlayIterator = function () {

        var _ = this;

        if (_.options.infinite === false) {

            if (_.direction === 1) {

                if ((_.currentSlide + 1) === _.slideCount -
                    1) {
                    _.direction = 0;
                }

                _.slideHandler(_.currentSlide + _.options.slidesToScroll);

            } else {

                if ((_.currentSlide - 1 === 0)) {

                    _.direction = 1;

                }

                _.slideHandler(_.currentSlide - _.options.slidesToScroll);

            }

        } else {

            _.slideHandler(_.currentSlide + _.options.slidesToScroll);

        }

    };

    Slick.prototype.buildArrows = function () {

        var _ = this;

        if (_.options.arrows === true) {

            _.$prevArrow = $(_.options.prevArrow).addClass('slick-arrow');
            _.$nextArrow = $(_.options.nextArrow).addClass('slick-arrow');

            if (_.slideCount > _.options.slidesToShow) {

                _.$prevArrow.removeClass('slick-hidden').removeAttr('aria-hidden tabindex');
                _.$nextArrow.removeClass('slick-hidden').removeAttr('aria-hidden tabindex');

                if (_.htmlExpr.test(_.options.prevArrow)) {
                    _.$prevArrow.prependTo(_.options.appendArrows);
                }

                if (_.htmlExpr.test(_.options.nextArrow)) {
                    _.$nextArrow.appendTo(_.options.appendArrows);
                }

                if (_.options.infinite !== true) {
                    _.$prevArrow
                        .addClass('slick-disabled')
                        .attr('aria-disabled', 'true');
                }

            } else {

                _.$prevArrow.add(_.$nextArrow)

                    .addClass('slick-hidden')
                    .attr({
                        'aria-disabled': 'true',
                        'tabindex': '-1'
                    });

            }

        }

    };

    Slick.prototype.buildDots = function () {

        var _ = this,
            i, dotString;

        if (_.options.dots === true && _.slideCount > _.options.slidesToShow) {

            dotString = '<ul class="' + _.options.dotsClass + '">';

            for (i = 0; i <= _.getDotCount(); i += 1) {
                dotString += '<li>' + _.options.customPaging.call(this, _, i) + '</li>';
            }

            dotString += '</ul>';

            _.$dots = $(dotString).appendTo(
                _.options.appendDots);

            _.$dots.find('li').first().addClass('slick-active').attr('aria-hidden', 'false');

        }

    };

    Slick.prototype.buildOut = function () {

        var _ = this;

        _.$slides =
            _.$slider
                .children(_.options.slide + ':not(.slick-cloned)')
                .addClass('slick-slide');

        _.slideCount = _.$slides.length;

        _.$slides.each(function (index, element) {
            $(element)
                .attr('data-slick-index', index)
                .data('originalStyling', $(element).attr('style') || '');
        });

        _.$slider.addClass('slick-slider');

        _.$slideTrack = (_.slideCount === 0) ?
            $('<div class="slick-track"/>').appendTo(_.$slider) :
            _.$slides.wrapAll('<div class="slick-track"/>').parent();

        _.$list = _.$slideTrack.wrap(
            '<div aria-live="polite" class="slick-list"/>').parent();
        _.$slideTrack.css('opacity', 0);

        if (_.options.centerMode === true || _.options.swipeToSlide === true) {
            _.options.slidesToScroll = 1;
        }

        $('img[data-lazy]', _.$slider).not('[src]').addClass('slick-loading');

        _.setupInfinite();

        _.buildArrows();

        _.buildDots();

        _.updateDots();


        _.setSlideClasses(typeof _.currentSlide === 'number' ? _.currentSlide : 0);

        if (_.options.draggable === true) {
            _.$list.addClass('draggable');
        }

    };

    Slick.prototype.buildRows = function () {

        var _ = this, a, b, c, newSlides, numOfSlides, originalSlides, slidesPerSection;

        newSlides = document.createDocumentFragment();
        originalSlides = _.$slider.children();

        if (_.options.rows > 1) {

            slidesPerSection = _.options.slidesPerRow * _.options.rows;
            numOfSlides = Math.ceil(
                originalSlides.length / slidesPerSection
            );

            for (a = 0; a < numOfSlides; a++) {
                var slide = document.createElement('div');
                for (b = 0; b < _.options.rows; b++) {
                    var row = document.createElement('div');
                    for (c = 0; c < _.options.slidesPerRow; c++) {
                        var target = (a * slidesPerSection + ((b * _.options.slidesPerRow) + c));
                        if (originalSlides.get(target)) {
                            row.appendChild(originalSlides.get(target));
                        }
                    }
                    slide.appendChild(row);
                }
                newSlides.appendChild(slide);
            }

            _.$slider.html(newSlides);
            _.$slider.children().children().children()
                .css({
                    'width': (100 / _.options.slidesPerRow) + '%',
                    'display': 'inline-block'
                });

        }

    };

    Slick.prototype.checkResponsive = function (initial, forceUpdate) {

        var _ = this,
            breakpoint, targetBreakpoint, respondToWidth, triggerBreakpoint = false;
        var sliderWidth = _.$slider.width();
        var windowWidth = window.innerWidth || $(window).width();

        if (_.respondTo === 'window') {
            respondToWidth = windowWidth;
        } else if (_.respondTo === 'slider') {
            respondToWidth = sliderWidth;
        } else if (_.respondTo === 'min') {
            respondToWidth = Math.min(windowWidth, sliderWidth);
        }

        if (_.options.responsive &&
            _.options.responsive.length &&
            _.options.responsive !== null) {

            targetBreakpoint = null;

            for (breakpoint in _.breakpoints) {
                if (_.breakpoints.hasOwnProperty(breakpoint)) {
                    if (_.originalSettings.mobileFirst === false) {
                        if (respondToWidth < _.breakpoints[breakpoint]) {
                            targetBreakpoint = _.breakpoints[breakpoint];
                        }
                    } else {
                        if (respondToWidth > _.breakpoints[breakpoint]) {
                            targetBreakpoint = _.breakpoints[breakpoint];
                        }
                    }
                }
            }

            if (targetBreakpoint !== null) {
                if (_.activeBreakpoint !== null) {
                    if (targetBreakpoint !== _.activeBreakpoint || forceUpdate) {
                        _.activeBreakpoint =
                            targetBreakpoint;
                        if (_.breakpointSettings[targetBreakpoint] === 'unslick') {
                            _.unslick(targetBreakpoint);
                        } else {
                            _.options = $.extend({}, _.originalSettings,
                                _.breakpointSettings[
                                targetBreakpoint]);
                            if (initial === true) {
                                _.currentSlide = _.options.initialSlide;
                            }
                            _.refresh(initial);
                        }
                        triggerBreakpoint = targetBreakpoint;
                    }
                } else {
                    _.activeBreakpoint = targetBreakpoint;
                    if (_.breakpointSettings[targetBreakpoint] === 'unslick') {
                        _.unslick(targetBreakpoint);
                    } else {
                        _.options = $.extend({}, _.originalSettings,
                            _.breakpointSettings[
                            targetBreakpoint]);
                        if (initial === true) {
                            _.currentSlide = _.options.initialSlide;
                        }
                        _.refresh(initial);
                    }
                    triggerBreakpoint = targetBreakpoint;
                }
            } else {
                if (_.activeBreakpoint !== null) {
                    _.activeBreakpoint = null;
                    _.options = _.originalSettings;
                    if (initial === true) {
                        _.currentSlide = _.options.initialSlide;
                    }
                    _.refresh(initial);
                    triggerBreakpoint = targetBreakpoint;
                }
            }

            // only trigger breakpoints during an actual break. not on initialize.
            if (!initial && triggerBreakpoint !== false) {
                _.$slider.trigger('breakpoint', [_, triggerBreakpoint]);
            }
        }

    };

    Slick.prototype.changeSlide = function (event, dontAnimate) {

        var _ = this,
            $target = $(event.target),
            indexOffset, slideOffset, unevenOffset;

        // If target is a link, prevent default action.
        if ($target.is('a')) {
            event.preventDefault();
        }

        // If target is not the <li> element (ie: a child), find the <li>.
        if (!$target.is('li')) {
            $target = $target.closest('li');
        }

        unevenOffset = (_.slideCount % _.options.slidesToScroll !== 0);
        indexOffset = unevenOffset ? 0 : (_.slideCount - _.currentSlide) % _.options.slidesToScroll;

        switch (event.data.message) {

            case 'previous':
                slideOffset = indexOffset === 0 ? _.options.slidesToScroll : _.options.slidesToShow - indexOffset;
                if (_.slideCount > _.options.slidesToShow) {
                    _.slideHandler(_.currentSlide - slideOffset, false, dontAnimate);
                }
                break;

            case 'next':
                slideOffset = indexOffset === 0 ? _.options.slidesToScroll : indexOffset;
                if (_.slideCount > _.options.slidesToShow) {
                    _.slideHandler(_.currentSlide + slideOffset, false, dontAnimate);
                }
                break;

            case 'index':
                var index = event.data.index === 0 ? 0 :
                    event.data.index || $target.index() * _.options.slidesToScroll;

                _.slideHandler(_.checkNavigable(index), false, dontAnimate);
                $target.children().trigger('focus');
                break;

            default:
                return;
        }

    };

    Slick.prototype.checkNavigable = function (index) {

        var _ = this,
            navigables, prevNavigable;

        navigables = _.getNavigableIndexes();
        prevNavigable = 0;
        if (index > navigables[navigables.length - 1]) {
            index = navigables[navigables.length - 1];
        } else {
            for (var n in navigables) {
                if (index < navigables[n]) {
                    index = prevNavigable;
                    break;
                }
                prevNavigable = navigables[n];
            }
        }

        return index;
    };

    Slick.prototype.cleanUpEvents = function () {

        var _ = this;

        if (_.options.dots && _.$dots !== null) {

            $('li', _.$dots).off('click.slick', _.changeSlide);

            if (_.options.pauseOnDotsHover === true && _.options.autoplay === true) {

                $('li', _.$dots)
                    .off('mouseenter.slick', $.proxy(_.setPaused, _, true))
                    .off('mouseleave.slick', $.proxy(_.setPaused, _, false));

            }

        }

        if (_.options.arrows === true && _.slideCount > _.options.slidesToShow) {
            _.$prevArrow && _.$prevArrow.off('click.slick', _.changeSlide);
            _.$nextArrow && _.$nextArrow.off('click.slick', _.changeSlide);
        }

        _.$list.off('touchstart.slick mousedown.slick', _.swipeHandler);
        _.$list.off('touchmove.slick mousemove.slick', _.swipeHandler);
        _.$list.off('touchend.slick mouseup.slick', _.swipeHandler);
        _.$list.off('touchcancel.slick mouseleave.slick', _.swipeHandler);

        _.$list.off('click.slick', _.clickHandler);

        $(document).off(_.visibilityChange, _.visibility);

        _.$list.off('mouseenter.slick', $.proxy(_.setPaused, _, true));
        _.$list.off('mouseleave.slick', $.proxy(_.setPaused, _, false));

        if (_.options.accessibility === true) {
            _.$list.off('keydown.slick', _.keyHandler);
        }

        if (_.options.focusOnSelect === true) {
            $(_.$slideTrack).children().off('click.slick', _.selectHandler);
        }

        $(window).off('orientationchange.slick.slick-' + _.instanceUid, _.orientationChange);

        $(window).off('resize.slick.slick-' + _.instanceUid, _.resize);

        $('[draggable!=true]', _.$slideTrack).off('dragstart', _.preventDefault);

        $(window).off('load.slick.slick-' + _.instanceUid, _.setPosition);
        $(document).off('ready.slick.slick-' + _.instanceUid, _.setPosition);
    };

    Slick.prototype.cleanUpRows = function () {

        var _ = this, originalSlides;

        if (_.options.rows > 1) {
            originalSlides = _.$slides.children().children();
            originalSlides.removeAttr('style');
            _.$slider.html(originalSlides);
        }

    };

    Slick.prototype.clickHandler = function (event) {

        var _ = this;

        if (_.shouldClick === false) {
            event.stopImmediatePropagation();
            event.stopPropagation();
            event.preventDefault();
        }

    };

    Slick.prototype.destroy = function (refresh) {

        var _ = this;

        _.autoPlayClear();

        _.touchObject = {};

        _.cleanUpEvents();

        $('.slick-cloned', _.$slider).detach();

        if (_.$dots) {
            _.$dots.remove();
        }


        if (_.$prevArrow && _.$prevArrow.length) {

            _.$prevArrow
                .removeClass('slick-disabled slick-arrow slick-hidden')
                .removeAttr('aria-hidden aria-disabled tabindex')
                .css("display", "");

            if (_.htmlExpr.test(_.options.prevArrow)) {
                _.$prevArrow.remove();
            }
        }

        if (_.$nextArrow && _.$nextArrow.length) {

            _.$nextArrow
                .removeClass('slick-disabled slick-arrow slick-hidden')
                .removeAttr('aria-hidden aria-disabled tabindex')
                .css("display", "");

            if (_.htmlExpr.test(_.options.nextArrow)) {
                _.$nextArrow.remove();
            }

        }


        if (_.$slides) {

            _.$slides
                .removeClass('slick-slide slick-active slick-center slick-visible slick-current')
                .removeAttr('aria-hidden')
                .removeAttr('data-slick-index')
                .each(function () {
                    $(this).attr('style', $(this).data('originalStyling'));
                });

            _.$slideTrack.children(this.options.slide).detach();

            _.$slideTrack.detach();

            _.$list.detach();

            _.$slider.append(_.$slides);
        }

        _.cleanUpRows();

        _.$slider.removeClass('slick-slider');
        _.$slider.removeClass('slick-initialized');

        _.unslicked = true;

        if (!refresh) {
            _.$slider.trigger('destroy', [_]);
        }

    };

    Slick.prototype.disableTransition = function (slide) {

        var _ = this,
            transition = {};

        transition[_.transitionType] = '';

        if (_.options.fade === false) {
            _.$slideTrack.css(transition);
        } else {
            _.$slides.eq(slide).css(transition);
        }

    };

    Slick.prototype.fadeSlide = function (slideIndex, callback) {

        var _ = this;

        if (_.cssTransitions === false) {

            _.$slides.eq(slideIndex).css({
                zIndex: _.options.zIndex
            });

            _.$slides.eq(slideIndex).animate({
                opacity: 1
            }, _.options.speed, _.options.easing, callback);

        } else {

            _.applyTransition(slideIndex);

            _.$slides.eq(slideIndex).css({
                opacity: 1,
                zIndex: _.options.zIndex
            });

            if (callback) {
                setTimeout(function () {

                    _.disableTransition(slideIndex);

                    callback.call();
                }, _.options.speed);
            }

        }

    };

    Slick.prototype.fadeSlideOut = function (slideIndex) {

        var _ = this;

        if (_.cssTransitions === false) {

            _.$slides.eq(slideIndex).animate({
                opacity: 0,
                zIndex: _.options.zIndex - 2
            }, _.options.speed, _.options.easing);

        } else {

            _.applyTransition(slideIndex);

            _.$slides.eq(slideIndex).css({
                opacity: 0,
                zIndex: _.options.zIndex - 2
            });

        }

    };

    Slick.prototype.filterSlides = Slick.prototype.slickFilter = function (filter) {

        var _ = this;

        if (filter !== null) {

            _.$slidesCache = _.$slides;

            _.unload();

            _.$slideTrack.children(this.options.slide).detach();

            _.$slidesCache.filter(filter).appendTo(_.$slideTrack);

            _.reinit();

        }

    };

    Slick.prototype.getCurrent = Slick.prototype.slickCurrentSlide = function () {

        var _ = this;
        return _.currentSlide;

    };

    Slick.prototype.getDotCount = function () {

        var _ = this;

        var breakPoint = 0;
        var counter = 0;
        var pagerQty = 0;

        if (_.options.infinite === true) {
            while (breakPoint < _.slideCount) {
                ++pagerQty;
                breakPoint = counter + _.options.slidesToScroll;
                counter += _.options.slidesToScroll <= _.options.slidesToShow ? _.options.slidesToScroll : _.options.slidesToShow;
            }
        } else if (_.options.centerMode === true) {
            pagerQty = _.slideCount;
        } else {
            while (breakPoint < _.slideCount) {
                ++pagerQty;
                breakPoint = counter + _.options.slidesToScroll;
                counter += _.options.slidesToScroll <= _.options.slidesToShow ? _.options.slidesToScroll : _.options.slidesToShow;
            }
        }

        return pagerQty - 1;

    };

    Slick.prototype.getLeft = function (slideIndex) {

        var _ = this,
            targetLeft,
            verticalHeight,
            verticalOffset = 0,
            targetSlide;

        _.slideOffset = 0;
        verticalHeight = _.$slides.first().outerHeight(true);

        if (_.options.infinite === true) {
            if (_.slideCount > _.options.slidesToShow) {
                _.slideOffset = (_.slideWidth * _.options.slidesToShow) * -1;
                verticalOffset = (verticalHeight * _.options.slidesToShow) * -1;
            }
            if (_.slideCount % _.options.slidesToScroll !== 0) {
                if (slideIndex + _.options.slidesToScroll > _.slideCount && _.slideCount > _.options.slidesToShow) {
                    if (slideIndex > _.slideCount) {
                        _.slideOffset = ((_.options.slidesToShow - (slideIndex - _.slideCount)) * _.slideWidth) * -1;
                        verticalOffset = ((_.options.slidesToShow - (slideIndex - _.slideCount)) * verticalHeight) * -1;
                    } else {
                        _.slideOffset = ((_.slideCount % _.options.slidesToScroll) * _.slideWidth) * -1;
                        verticalOffset = ((_.slideCount % _.options.slidesToScroll) * verticalHeight) * -1;
                    }
                }
            }
        } else {
            if (slideIndex + _.options.slidesToShow > _.slideCount) {
                _.slideOffset = ((slideIndex + _.options.slidesToShow) - _.slideCount) * _.slideWidth;
                verticalOffset = ((slideIndex + _.options.slidesToShow) - _.slideCount) * verticalHeight;
            }
        }

        if (_.slideCount <= _.options.slidesToShow) {
            _.slideOffset = 0;
            verticalOffset = 0;
        }

        if (_.options.centerMode === true && _.options.infinite === true) {
            _.slideOffset += _.slideWidth * Math.floor(_.options.slidesToShow / 2) - _.slideWidth;
        } else if (_.options.centerMode === true) {
            _.slideOffset = 0;
            _.slideOffset += _.slideWidth * Math.floor(_.options.slidesToShow / 2);
        }

        if (_.options.vertical === false) {
            targetLeft = ((slideIndex * _.slideWidth) * -1) + _.slideOffset;
        } else {
            targetLeft = ((slideIndex * verticalHeight) * -1) + verticalOffset;
        }

        if (_.options.variableWidth === true) {

            if (_.slideCount <= _.options.slidesToShow || _.options.infinite === false) {
                targetSlide = _.$slideTrack.children('.slick-slide').eq(slideIndex);
            } else {
                targetSlide = _.$slideTrack.children('.slick-slide').eq(slideIndex + _.options.slidesToShow);
            }

            if (_.options.rtl === true) {
                if (targetSlide[0]) {
                    targetLeft = (_.$slideTrack.width() - targetSlide[0].offsetLeft - targetSlide.width()) * -1;
                } else {
                    targetLeft = 0;
                }
            } else {
                targetLeft = targetSlide[0] ? targetSlide[0].offsetLeft * -1 : 0;
            }

            if (_.options.centerMode === true) {
                if (_.slideCount <= _.options.slidesToShow || _.options.infinite === false) {
                    targetSlide = _.$slideTrack.children('.slick-slide').eq(slideIndex);
                } else {
                    targetSlide = _.$slideTrack.children('.slick-slide').eq(slideIndex + _.options.slidesToShow + 1);
                }

                if (_.options.rtl === true) {
                    if (targetSlide[0]) {
                        targetLeft = (_.$slideTrack.width() - targetSlide[0].offsetLeft - targetSlide.width()) * -1;
                    } else {
                        targetLeft = 0;
                    }
                } else {
                    targetLeft = targetSlide[0] ? targetSlide[0].offsetLeft * -1 : 0;
                }

                targetLeft += (_.$list.width() - targetSlide.outerWidth()) / 2;
            }
        }

        return targetLeft;

    };

    Slick.prototype.getOption = Slick.prototype.slickGetOption = function (option) {

        var _ = this;

        return _.options[option];

    };

    Slick.prototype.getNavigableIndexes = function () {

        var _ = this,
            breakPoint = 0,
            counter = 0,
            indexes = [],
            max;

        if (_.options.infinite === false) {
            max = _.slideCount;
        } else {
            breakPoint = _.options.slidesToScroll * -1;
            counter = _.options.slidesToScroll * -1;
            max = _.slideCount * 2;
        }

        while (breakPoint < max) {
            indexes.push(breakPoint);
            breakPoint = counter + _.options.slidesToScroll;
            counter += _.options.slidesToScroll <= _.options.slidesToShow ? _.options.slidesToScroll : _.options.slidesToShow;
        }

        return indexes;

    };

    Slick.prototype.getSlick = function () {

        return this;

    };

    Slick.prototype.getSlideCount = function () {

        var _ = this,
            slidesTraversed, swipedSlide, centerOffset;

        centerOffset = _.options.centerMode === true ? _.slideWidth * Math.floor(_.options.slidesToShow / 2) : 0;

        if (_.options.swipeToSlide === true) {
            _.$slideTrack.find('.slick-slide').each(function (index, slide) {
                if (slide.offsetLeft - centerOffset + ($(slide).outerWidth() / 2) > (_.swipeLeft * -1)) {
                    swipedSlide = slide;
                    return false;
                }
            });

            slidesTraversed = Math.abs($(swipedSlide).attr('data-slick-index') - _.currentSlide) || 1;

            return slidesTraversed;

        } else {
            return _.options.slidesToScroll;
        }

    };

    Slick.prototype.goTo = Slick.prototype.slickGoTo = function (slide, dontAnimate) {

        var _ = this;

        _.changeSlide({
            data: {
                message: 'index',
                index: parseInt(slide)
            }
        }, dontAnimate);

    };

    Slick.prototype.init = function (creation) {

        var _ = this;

        if (!$(_.$slider).hasClass('slick-initialized')) {

            $(_.$slider).addClass('slick-initialized');

            _.buildRows();
            _.buildOut();
            _.setProps();
            _.startLoad();
            _.loadSlider();
            _.initializeEvents();
            _.updateArrows();
            _.updateDots();

        }

        if (creation) {
            _.$slider.trigger('init', [_]);
        }

        if (_.options.accessibility === true) {
            _.initADA();
        }

    };

    Slick.prototype.initArrowEvents = function () {

        var _ = this;

        if (_.options.arrows === true && _.slideCount > _.options.slidesToShow) {
            _.$prevArrow.on('click.slick', {
                message: 'previous'
            }, _.changeSlide);
            _.$nextArrow.on('click.slick', {
                message: 'next'
            }, _.changeSlide);
        }

    };

    Slick.prototype.initDotEvents = function () {

        var _ = this;

        if (_.options.dots === true && _.slideCount > _.options.slidesToShow) {
            $('li', _.$dots).on('click.slick', {
                message: 'index'
            }, _.changeSlide);
        }

        if (_.options.dots === true && _.options.pauseOnDotsHover === true && _.options.autoplay === true) {
            $('li', _.$dots)
                .on('mouseenter.slick', $.proxy(_.setPaused, _, true))
                .on('mouseleave.slick', $.proxy(_.setPaused, _, false));
        }

    };

    Slick.prototype.initializeEvents = function () {

        var _ = this;

        _.initArrowEvents();

        _.initDotEvents();

        _.$list.on('touchstart.slick mousedown.slick', {
            action: 'start'
        }, _.swipeHandler);
        _.$list.on('touchmove.slick mousemove.slick', {
            action: 'move'
        }, _.swipeHandler);
        _.$list.on('touchend.slick mouseup.slick', {
            action: 'end'
        }, _.swipeHandler);
        _.$list.on('touchcancel.slick mouseleave.slick', {
            action: 'end'
        }, _.swipeHandler);

        _.$list.on('click.slick', _.clickHandler);

        $(document).on(_.visibilityChange, $.proxy(_.visibility, _));

        _.$list.on('mouseenter.slick', $.proxy(_.setPaused, _, true));
        _.$list.on('mouseleave.slick', $.proxy(_.setPaused, _, false));

        if (_.options.accessibility === true) {
            _.$list.on('keydown.slick', _.keyHandler);
        }

        if (_.options.focusOnSelect === true) {
            $(_.$slideTrack).children().on('click.slick', _.selectHandler);
        }

        $(window).on('orientationchange.slick.slick-' + _.instanceUid, $.proxy(_.orientationChange, _));

        $(window).on('resize.slick.slick-' + _.instanceUid, $.proxy(_.resize, _));

        $('[draggable!=true]', _.$slideTrack).on('dragstart', _.preventDefault);

        $(window).on('load.slick.slick-' + _.instanceUid, _.setPosition);
        $(document).on('ready.slick.slick-' + _.instanceUid, _.setPosition);

    };

    Slick.prototype.initUI = function () {

        var _ = this;

        if (_.options.arrows === true && _.slideCount > _.options.slidesToShow) {

            _.$prevArrow.show();
            _.$nextArrow.show();

        }

        if (_.options.dots === true && _.slideCount > _.options.slidesToShow) {

            _.$dots.show();

        }

        if (_.options.autoplay === true) {

            _.autoPlay();

        }

    };

    Slick.prototype.keyHandler = function (event) {

        var _ = this;
        //Dont slide if the cursor is inside the form fields and arrow keys are pressed
        if (!event.target.tagName.match('TEXTAREA|INPUT|SELECT')) {
            if (event.keyCode === 37 && _.options.accessibility === true) {
                _.changeSlide({
                    data: {
                        message: 'previous'
                    }
                });
            } else if (event.keyCode === 39 && _.options.accessibility === true) {
                _.changeSlide({
                    data: {
                        message: 'next'
                    }
                });
            }
        }

    };

    Slick.prototype.lazyLoad = function () {

        var _ = this,
            loadRange, cloneRange, rangeStart, rangeEnd;

        function loadImages(imagesScope) {
            $('img[data-lazy]', imagesScope).each(function () {

                var image = $(this),
                    imageSource = $(this).attr('data-lazy'),
                    imageToLoad = document.createElement('img');

                imageToLoad.onload = function () {
                    image
                        .animate({ opacity: 0 }, 100, function () {
                            image
                                .attr('src', imageSource)
                                .animate({ opacity: 1 }, 200, function () {
                                    image
                                        .removeAttr('data-lazy')
                                        .removeClass('slick-loading');
                                });
                        });
                };

                imageToLoad.src = imageSource;

            });
        }

        if (_.options.centerMode === true) {
            if (_.options.infinite === true) {
                rangeStart = _.currentSlide + (_.options.slidesToShow / 2 + 1);
                rangeEnd = rangeStart + _.options.slidesToShow + 2;
            } else {
                rangeStart = Math.max(0, _.currentSlide - (_.options.slidesToShow / 2 + 1));
                rangeEnd = 2 + (_.options.slidesToShow / 2 + 1) + _.currentSlide;
            }
        } else {
            rangeStart = _.options.infinite ? _.options.slidesToShow + _.currentSlide : _.currentSlide;
            rangeEnd = rangeStart + _.options.slidesToShow;
            if (_.options.fade === true) {
                if (rangeStart > 0) rangeStart--;
                if (rangeEnd <= _.slideCount) rangeEnd++;
            }
        }

        loadRange = _.$slider.find('.slick-slide').slice(rangeStart, rangeEnd);
        loadImages(loadRange);

        if (_.slideCount <= _.options.slidesToShow) {
            cloneRange = _.$slider.find('.slick-slide');
            loadImages(cloneRange);
        } else
            if (_.currentSlide >= _.slideCount - _.options.slidesToShow) {
                cloneRange = _.$slider.find('.slick-cloned').slice(0, _.options.slidesToShow);
                loadImages(cloneRange);
            } else if (_.currentSlide === 0) {
                cloneRange = _.$slider.find('.slick-cloned').slice(_.options.slidesToShow * -1);
                loadImages(cloneRange);
            }

    };

    Slick.prototype.loadSlider = function () {

        var _ = this;

        _.setPosition();

        _.$slideTrack.css({
            opacity: 1
        });

        _.$slider.removeClass('slick-loading');

        _.initUI();

        if (_.options.lazyLoad === 'progressive') {
            _.progressiveLazyLoad();
        }

    };

    Slick.prototype.next = Slick.prototype.slickNext = function () {

        var _ = this;

        _.changeSlide({
            data: {
                message: 'next'
            }
        });

    };

    Slick.prototype.orientationChange = function () {

        var _ = this;

        _.checkResponsive();
        _.setPosition();

    };

    Slick.prototype.pause = Slick.prototype.slickPause = function () {

        var _ = this;

        _.autoPlayClear();
        _.paused = true;

    };

    Slick.prototype.play = Slick.prototype.slickPlay = function () {

        var _ = this;

        _.paused = false;
        _.autoPlay();

    };

    Slick.prototype.postSlide = function (index) {

        var _ = this;

        _.$slider.trigger('afterChange', [_, index]);

        _.animating = false;

        _.setPosition();

        _.swipeLeft = null;

        if (_.options.autoplay === true && _.paused === false) {
            _.autoPlay();
        }
        if (_.options.accessibility === true) {
            _.initADA();
        }

    };

    Slick.prototype.prev = Slick.prototype.slickPrev = function () {

        var _ = this;

        _.changeSlide({
            data: {
                message: 'previous'
            }
        });

    };

    Slick.prototype.preventDefault = function (event) {
        event.preventDefault();
    };

    Slick.prototype.progressiveLazyLoad = function () {

        var _ = this,
            imgCount, targetImage;

        imgCount = $('img[data-lazy]', _.$slider).length;

        if (imgCount > 0) {
            targetImage = $('img[data-lazy]', _.$slider).first();
            targetImage.attr('src', null);
            targetImage.attr('src', targetImage.attr('data-lazy')).removeClass('slick-loading').load(function () {
                targetImage.removeAttr('data-lazy');
                _.progressiveLazyLoad();

                if (_.options.adaptiveHeight === true) {
                    _.setPosition();
                }
            })
                .error(function () {
                    targetImage.removeAttr('data-lazy');
                    _.progressiveLazyLoad();
                });
        }

    };

    Slick.prototype.refresh = function (initializing) {

        var _ = this, currentSlide, firstVisible;

        firstVisible = _.slideCount - _.options.slidesToShow;

        // check that the new breakpoint can actually accept the
        // "current slide" as the current slide, otherwise we need
        // to set it to the closest possible value.
        if (!_.options.infinite) {
            if (_.slideCount <= _.options.slidesToShow) {
                _.currentSlide = 0;
            } else if (_.currentSlide > firstVisible) {
                _.currentSlide = firstVisible;
            }
        }

        currentSlide = _.currentSlide;

        _.destroy(true);

        $.extend(_, _.initials, { currentSlide: currentSlide });

        _.init();

        if (!initializing) {

            _.changeSlide({
                data: {
                    message: 'index',
                    index: currentSlide
                }
            }, false);

        }

    };

    Slick.prototype.registerBreakpoints = function () {

        var _ = this, breakpoint, currentBreakpoint, l,
            responsiveSettings = _.options.responsive || null;

        if ($.type(responsiveSettings) === "array" && responsiveSettings.length) {

            _.respondTo = _.options.respondTo || 'window';

            for (breakpoint in responsiveSettings) {

                l = _.breakpoints.length - 1;
                currentBreakpoint = responsiveSettings[breakpoint].breakpoint;

                if (responsiveSettings.hasOwnProperty(breakpoint)) {

                    // loop through the breakpoints and cut out any existing
                    // ones with the same breakpoint number, we don't want dupes.
                    while (l >= 0) {
                        if (_.breakpoints[l] && _.breakpoints[l] === currentBreakpoint) {
                            _.breakpoints.splice(l, 1);
                        }
                        l--;
                    }

                    _.breakpoints.push(currentBreakpoint);
                    _.breakpointSettings[currentBreakpoint] = responsiveSettings[breakpoint].settings;

                }

            }

            _.breakpoints.sort(function (a, b) {
                return (_.options.mobileFirst) ? a - b : b - a;
            });

        }

    };

    Slick.prototype.reinit = function () {

        var _ = this;

        _.$slides =
            _.$slideTrack
                .children(_.options.slide)
                .addClass('slick-slide');

        _.slideCount = _.$slides.length;

        if (_.currentSlide >= _.slideCount && _.currentSlide !== 0) {
            _.currentSlide = _.currentSlide - _.options.slidesToScroll;
        }

        if (_.slideCount <= _.options.slidesToShow) {
            _.currentSlide = 0;
        }

        _.registerBreakpoints();

        _.setProps();
        _.setupInfinite();
        _.buildArrows();
        _.updateArrows();
        _.initArrowEvents();
        _.buildDots();
        _.updateDots();
        _.initDotEvents();

        _.checkResponsive(false, true);

        if (_.options.focusOnSelect === true) {
            $(_.$slideTrack).children().on('click.slick', _.selectHandler);
        }

        _.setSlideClasses(0);

        _.setPosition();

        _.$slider.trigger('reInit', [_]);

        if (_.options.autoplay === true) {
            _.focusHandler();
        }

    };

    Slick.prototype.resize = function () {

        var _ = this;

        if ($(window).width() !== _.windowWidth) {
            clearTimeout(_.windowDelay);
            _.windowDelay = window.setTimeout(function () {
                _.windowWidth = $(window).width();
                _.checkResponsive();
                if (!_.unslicked) { _.setPosition(); }
            }, 50);
        }
    };

    Slick.prototype.removeSlide = Slick.prototype.slickRemove = function (index, removeBefore, removeAll) {

        var _ = this;

        if (typeof (index) === 'boolean') {
            removeBefore = index;
            index = removeBefore === true ? 0 : _.slideCount - 1;
        } else {
            index = removeBefore === true ? --index : index;
        }

        if (_.slideCount < 1 || index < 0 || index > _.slideCount - 1) {
            return false;
        }

        _.unload();

        if (removeAll === true) {
            _.$slideTrack.children().remove();
        } else {
            _.$slideTrack.children(this.options.slide).eq(index).remove();
        }

        _.$slides = _.$slideTrack.children(this.options.slide);

        _.$slideTrack.children(this.options.slide).detach();

        _.$slideTrack.append(_.$slides);

        _.$slidesCache = _.$slides;

        _.reinit();

    };

    Slick.prototype.setCSS = function (position) {

        var _ = this,
            positionProps = {},
            x, y;

        if (_.options.rtl === true) {
            position = -position;
        }
        x = _.positionProp == 'left' ? Math.ceil(position) + 'px' : '0px';
        y = _.positionProp == 'top' ? Math.ceil(position) + 'px' : '0px';

        positionProps[_.positionProp] = position;

        if (_.transformsEnabled === false) {
            _.$slideTrack.css(positionProps);
        } else {
            positionProps = {};
            if (_.cssTransitions === false) {
                positionProps[_.animType] = 'translate(' + x + ', ' + y + ')';
                _.$slideTrack.css(positionProps);
            } else {
                positionProps[_.animType] = 'translate3d(' + x + ', ' + y + ', 0px)';
                _.$slideTrack.css(positionProps);
            }
        }

    };

    Slick.prototype.setDimensions = function () {

        var _ = this;

        if (_.options.vertical === false) {
            if (_.options.centerMode === true) {
                _.$list.css({
                    padding: ('0px ' + _.options.centerPadding)
                });
            }
        } else {
            _.$list.height(_.$slides.first().outerHeight(true) * _.options.slidesToShow);
            if (_.options.centerMode === true) {
                _.$list.css({
                    padding: (_.options.centerPadding + ' 0px')
                });
            }
        }

        _.listWidth = _.$list.width();
        _.listHeight = _.$list.height();


        if (_.options.vertical === false && _.options.variableWidth === false) {
            _.slideWidth = Math.ceil(_.listWidth / _.options.slidesToShow);
            _.$slideTrack.width(Math.ceil((_.slideWidth * _.$slideTrack.children('.slick-slide').length)));

        } else if (_.options.variableWidth === true) {
            _.$slideTrack.width(5000 * _.slideCount);
        } else {
            _.slideWidth = Math.ceil(_.listWidth);
            _.$slideTrack.height(Math.ceil((_.$slides.first().outerHeight(true) * _.$slideTrack.children('.slick-slide').length)));
        }

        var offset = _.$slides.first().outerWidth(true) - _.$slides.first().width();
        if (_.options.variableWidth === false) _.$slideTrack.children('.slick-slide').width(_.slideWidth - offset);

    };

    Slick.prototype.setFade = function () {

        var _ = this,
            targetLeft;

        _.$slides.each(function (index, element) {
            targetLeft = (_.slideWidth * index) * -1;
            if (_.options.rtl === true) {
                $(element).css({
                    position: 'relative',
                    right: targetLeft,
                    top: 0,
                    zIndex: _.options.zIndex - 2,
                    opacity: 0
                });
            } else {
                $(element).css({
                    position: 'relative',
                    left: targetLeft,
                    top: 0,
                    zIndex: _.options.zIndex - 2,
                    opacity: 0
                });
            }
        });

        _.$slides.eq(_.currentSlide).css({
            zIndex: _.options.zIndex - 1,
            opacity: 1
        });

    };

    Slick.prototype.setHeight = function () {

        var _ = this;

        if (_.options.slidesToShow === 1 && _.options.adaptiveHeight === true && _.options.vertical === false) {
            var targetHeight = _.$slides.eq(_.currentSlide).outerHeight(true);
            _.$list.css('height', targetHeight);
        }

    };

    Slick.prototype.setOption = Slick.prototype.slickSetOption = function (option, value, refresh) {

        var _ = this, l, item;

        if (option === "responsive" && $.type(value) === "array") {
            for (item in value) {
                if ($.type(_.options.responsive) !== "array") {
                    _.options.responsive = [value[item]];
                } else {
                    l = _.options.responsive.length - 1;
                    // loop through the responsive object and splice out duplicates.
                    while (l >= 0) {
                        if (_.options.responsive[l].breakpoint === value[item].breakpoint) {
                            _.options.responsive.splice(l, 1);
                        }
                        l--;
                    }
                    _.options.responsive.push(value[item]);
                }
            }
        } else {
            _.options[option] = value;
        }

        if (refresh === true) {
            _.unload();
            _.reinit();
        }

    };

    Slick.prototype.setPosition = function () {

        var _ = this;

        _.setDimensions();

        _.setHeight();

        if (_.options.fade === false) {
            _.setCSS(_.getLeft(_.currentSlide));
        } else {
            _.setFade();
        }

        _.$slider.trigger('setPosition', [_]);

    };

    Slick.prototype.setProps = function () {

        var _ = this,
            bodyStyle = document.body.style;

        _.positionProp = _.options.vertical === true ? 'top' : 'left';

        if (_.positionProp === 'top') {
            _.$slider.addClass('slick-vertical');
        } else {
            _.$slider.removeClass('slick-vertical');
        }

        if (bodyStyle.WebkitTransition !== undefined ||
            bodyStyle.MozTransition !== undefined ||
            bodyStyle.msTransition !== undefined) {
            if (_.options.useCSS === true) {
                _.cssTransitions = true;
            }
        }

        if (_.options.fade) {
            if (typeof _.options.zIndex === 'number') {
                if (_.options.zIndex < 3) {
                    _.options.zIndex = 3;
                }
            } else {
                _.options.zIndex = _.defaults.zIndex;
            }
        }

        if (bodyStyle.OTransform !== undefined) {
            _.animType = 'OTransform';
            _.transformType = '-o-transform';
            _.transitionType = 'OTransition';
            if (bodyStyle.perspectiveProperty === undefined && bodyStyle.webkitPerspective === undefined) _.animType = false;
        }
        if (bodyStyle.MozTransform !== undefined) {
            _.animType = 'MozTransform';
            _.transformType = '-moz-transform';
            _.transitionType = 'MozTransition';
            if (bodyStyle.perspectiveProperty === undefined && bodyStyle.MozPerspective === undefined) _.animType = false;
        }
        if (bodyStyle.webkitTransform !== undefined) {
            _.animType = 'webkitTransform';
            _.transformType = '-webkit-transform';
            _.transitionType = 'webkitTransition';
            if (bodyStyle.perspectiveProperty === undefined && bodyStyle.webkitPerspective === undefined) _.animType = false;
        }
        if (bodyStyle.msTransform !== undefined) {
            _.animType = 'msTransform';
            _.transformType = '-ms-transform';
            _.transitionType = 'msTransition';
            if (bodyStyle.msTransform === undefined) _.animType = false;
        }
        if (bodyStyle.transform !== undefined && _.animType !== false) {
            _.animType = 'transform';
            _.transformType = 'transform';
            _.transitionType = 'transition';
        }
        _.transformsEnabled = _.options.useTransform && (_.animType !== null && _.animType !== false);
    };


    Slick.prototype.setSlideClasses = function (index) {

        var _ = this,
            centerOffset, allSlides, indexOffset, remainder;

        allSlides = _.$slider
            .find('.slick-slide')
            .removeClass('slick-active slick-center slick-current')
            .attr('aria-hidden', 'true');

        _.$slides
            .eq(index)
            .addClass('slick-current');

        if (_.options.centerMode === true) {

            centerOffset = Math.floor(_.options.slidesToShow / 2);

            if (_.options.infinite === true) {

                if (index >= centerOffset && index <= (_.slideCount - 1) - centerOffset) {

                    _.$slides
                        .slice(index - centerOffset, index + centerOffset + 1)
                        .addClass('slick-active')
                        .attr('aria-hidden', 'false');

                } else {

                    indexOffset = _.options.slidesToShow + index;
                    allSlides
                        .slice(indexOffset - centerOffset + 1, indexOffset + centerOffset + 2)
                        .addClass('slick-active')
                        .attr('aria-hidden', 'false');

                }

                if (index === 0) {

                    allSlides
                        .eq(allSlides.length - 1 - _.options.slidesToShow)
                        .addClass('slick-center');

                } else if (index === _.slideCount - 1) {

                    allSlides
                        .eq(_.options.slidesToShow)
                        .addClass('slick-center');

                }

            }

            _.$slides
                .eq(index)
                .addClass('slick-center');

        } else {

            if (index >= 0 && index <= (_.slideCount - _.options.slidesToShow)) {

                _.$slides
                    .slice(index, index + _.options.slidesToShow)
                    .addClass('slick-active')
                    .attr('aria-hidden', 'false');

            } else if (allSlides.length <= _.options.slidesToShow) {

                allSlides
                    .addClass('slick-active')
                    .attr('aria-hidden', 'false');

            } else {

                remainder = _.slideCount % _.options.slidesToShow;
                indexOffset = _.options.infinite === true ? _.options.slidesToShow + index : index;

                if (_.options.slidesToShow == _.options.slidesToScroll && (_.slideCount - index) < _.options.slidesToShow) {

                    allSlides
                        .slice(indexOffset - (_.options.slidesToShow - remainder), indexOffset + remainder)
                        .addClass('slick-active')
                        .attr('aria-hidden', 'false');

                } else {

                    allSlides
                        .slice(indexOffset, indexOffset + _.options.slidesToShow)
                        .addClass('slick-active')
                        .attr('aria-hidden', 'false');

                }

            }

        }

        if (_.options.lazyLoad === 'ondemand') {
            _.lazyLoad();
        }

    };

    Slick.prototype.setupInfinite = function () {

        var _ = this,
            i, slideIndex, infiniteCount;

        if (_.options.fade === true) {
            _.options.centerMode = false;
        }

        if (_.options.infinite === true && _.options.fade === false) {

            slideIndex = null;

            if (_.slideCount > _.options.slidesToShow) {

                if (_.options.centerMode === true) {
                    infiniteCount = _.options.slidesToShow + 1;
                } else {
                    infiniteCount = _.options.slidesToShow;
                }

                for (i = _.slideCount; i > (_.slideCount -
                    infiniteCount); i -= 1) {
                    slideIndex = i - 1;
                    $(_.$slides[slideIndex]).clone(true).attr('id', '')
                        .attr('data-slick-index', slideIndex - _.slideCount)
                        .prependTo(_.$slideTrack).addClass('slick-cloned');
                }
                for (i = 0; i < infiniteCount; i += 1) {
                    slideIndex = i;
                    $(_.$slides[slideIndex]).clone(true).attr('id', '')
                        .attr('data-slick-index', slideIndex + _.slideCount)
                        .appendTo(_.$slideTrack).addClass('slick-cloned');
                }
                _.$slideTrack.find('.slick-cloned').find('[id]').each(function () {
                    $(this).attr('id', '');
                });

            }

        }

    };

    Slick.prototype.setPaused = function (paused) {

        var _ = this;

        if (_.options.autoplay === true && _.options.pauseOnHover === true) {
            _.paused = paused;
            if (!paused) {
                _.autoPlay();
            } else {
                _.autoPlayClear();
            }
        }
    };

    Slick.prototype.selectHandler = function (event) {

        var _ = this;

        var targetElement =
            $(event.target).is('.slick-slide') ?
                $(event.target) :
                $(event.target).parents('.slick-slide');

        var index = parseInt(targetElement.attr('data-slick-index'));

        if (!index) index = 0;

        if (_.slideCount <= _.options.slidesToShow) {

            _.setSlideClasses(index);
            _.asNavFor(index);
            return;

        }

        _.slideHandler(index);

    };

    Slick.prototype.slideHandler = function (index, sync, dontAnimate) {

        var targetSlide, animSlide, oldSlide, slideLeft, targetLeft = null,
            _ = this;

        sync = sync || false;

        if (_.animating === true && _.options.waitForAnimate === true) {
            return;
        }

        if (_.options.fade === true && _.currentSlide === index) {
            return;
        }

        if (_.slideCount <= _.options.slidesToShow) {
            return;
        }

        if (sync === false) {
            _.asNavFor(index);
        }

        targetSlide = index;
        targetLeft = _.getLeft(targetSlide);
        slideLeft = _.getLeft(_.currentSlide);

        _.currentLeft = _.swipeLeft === null ? slideLeft : _.swipeLeft;

        if (_.options.infinite === false && _.options.centerMode === false && (index < 0 || index > _.getDotCount() * _.options.slidesToScroll)) {
            if (_.options.fade === false) {
                targetSlide = _.currentSlide;
                if (dontAnimate !== true) {
                    _.animateSlide(slideLeft, function () {
                        _.postSlide(targetSlide);
                    });
                } else {
                    _.postSlide(targetSlide);
                }
            }
            return;
        } else if (_.options.infinite === false && _.options.centerMode === true && (index < 0 || index > (_.slideCount - _.options.slidesToScroll))) {
            if (_.options.fade === false) {
                targetSlide = _.currentSlide;
                if (dontAnimate !== true) {
                    _.animateSlide(slideLeft, function () {
                        _.postSlide(targetSlide);
                    });
                } else {
                    _.postSlide(targetSlide);
                }
            }
            return;
        }

        if (_.options.autoplay === true) {
            clearInterval(_.autoPlayTimer);
        }

        if (targetSlide < 0) {
            if (_.slideCount % _.options.slidesToScroll !== 0) {
                animSlide = _.slideCount - (_.slideCount % _.options.slidesToScroll);
            } else {
                animSlide = _.slideCount + targetSlide;
            }
        } else if (targetSlide >= _.slideCount) {
            if (_.slideCount % _.options.slidesToScroll !== 0) {
                animSlide = 0;
            } else {
                animSlide = targetSlide - _.slideCount;
            }
        } else {
            animSlide = targetSlide;
        }

        _.animating = true;

        _.$slider.trigger('beforeChange', [_, _.currentSlide, animSlide]);

        oldSlide = _.currentSlide;
        _.currentSlide = animSlide;

        _.setSlideClasses(_.currentSlide);

        _.updateDots();
        _.updateArrows();

        if (_.options.fade === true) {
            if (dontAnimate !== true) {

                _.fadeSlideOut(oldSlide);

                _.fadeSlide(animSlide, function () {
                    _.postSlide(animSlide);
                });

            } else {
                _.postSlide(animSlide);
            }
            _.animateHeight();
            return;
        }

        if (dontAnimate !== true) {
            _.animateSlide(targetLeft, function () {
                _.postSlide(animSlide);
            });
        } else {
            _.postSlide(animSlide);
        }

    };

    Slick.prototype.startLoad = function () {

        var _ = this;

        if (_.options.arrows === true && _.slideCount > _.options.slidesToShow) {

            _.$prevArrow.hide();
            _.$nextArrow.hide();

        }

        if (_.options.dots === true && _.slideCount > _.options.slidesToShow) {

            _.$dots.hide();

        }

        _.$slider.addClass('slick-loading');

    };

    Slick.prototype.swipeDirection = function () {

        var xDist, yDist, r, swipeAngle, _ = this;

        xDist = _.touchObject.startX - _.touchObject.curX;
        yDist = _.touchObject.startY - _.touchObject.curY;
        r = Math.atan2(yDist, xDist);

        swipeAngle = Math.round(r * 180 / Math.PI);
        if (swipeAngle < 0) {
            swipeAngle = 360 - Math.abs(swipeAngle);
        }

        if ((swipeAngle <= 45) && (swipeAngle >= 0)) {
            return (_.options.rtl === false ? 'left' : 'right');
        }
        if ((swipeAngle <= 360) && (swipeAngle >= 315)) {
            return (_.options.rtl === false ? 'left' : 'right');
        }
        if ((swipeAngle >= 135) && (swipeAngle <= 225)) {
            return (_.options.rtl === false ? 'right' : 'left');
        }
        if (_.options.verticalSwiping === true) {
            if ((swipeAngle >= 35) && (swipeAngle <= 135)) {
                return 'left';
            } else {
                return 'right';
            }
        }

        return 'vertical';

    };

    Slick.prototype.swipeEnd = function (event) {

        var _ = this,
            slideCount;

        _.dragging = false;

        _.shouldClick = (_.touchObject.swipeLength > 10) ? false : true;

        if (_.touchObject.curX === undefined) {
            return false;
        }

        if (_.touchObject.edgeHit === true) {
            _.$slider.trigger('edge', [_, _.swipeDirection()]);
        }

        if (_.touchObject.swipeLength >= _.touchObject.minSwipe) {

            switch (_.swipeDirection()) {
                case 'left':
                    slideCount = _.options.swipeToSlide ? _.checkNavigable(_.currentSlide + _.getSlideCount()) : _.currentSlide + _.getSlideCount();
                    _.slideHandler(slideCount);
                    _.currentDirection = 0;
                    _.touchObject = {};
                    _.$slider.trigger('swipe', [_, 'left']);
                    break;

                case 'right':
                    slideCount = _.options.swipeToSlide ? _.checkNavigable(_.currentSlide - _.getSlideCount()) : _.currentSlide - _.getSlideCount();
                    _.slideHandler(slideCount);
                    _.currentDirection = 1;
                    _.touchObject = {};
                    _.$slider.trigger('swipe', [_, 'right']);
                    break;
            }
        } else {
            if (_.touchObject.startX !== _.touchObject.curX) {
                _.slideHandler(_.currentSlide);
                _.touchObject = {};
            }
        }

    };

    Slick.prototype.swipeHandler = function (event) {

        var _ = this;

        if ((_.options.swipe === false) || ('ontouchend' in document && _.options.swipe === false)) {
            return;
        } else if (_.options.draggable === false && event.type.indexOf('mouse') !== -1) {
            return;
        }

        _.touchObject.fingerCount = event.originalEvent && event.originalEvent.touches !== undefined ?
            event.originalEvent.touches.length : 1;

        _.touchObject.minSwipe = _.listWidth / _.options
            .touchThreshold;

        if (_.options.verticalSwiping === true) {
            _.touchObject.minSwipe = _.listHeight / _.options
                .touchThreshold;
        }

        switch (event.data.action) {

            case 'start':
                _.swipeStart(event);
                break;

            case 'move':
                _.swipeMove(event);
                break;

            case 'end':
                _.swipeEnd(event);
                break;

        }

    };

    Slick.prototype.swipeMove = function (event) {

        var _ = this,
            edgeWasHit = false,
            curLeft, swipeDirection, swipeLength, positionOffset, touches;

        touches = event.originalEvent !== undefined ? event.originalEvent.touches : null;

        if (!_.dragging || touches && touches.length !== 1) {
            return false;
        }

        curLeft = _.getLeft(_.currentSlide);

        _.touchObject.curX = touches !== undefined ? touches[0].pageX : event.clientX;
        _.touchObject.curY = touches !== undefined ? touches[0].pageY : event.clientY;

        _.touchObject.swipeLength = Math.round(Math.sqrt(
            Math.pow(_.touchObject.curX - _.touchObject.startX, 2)));

        if (_.options.verticalSwiping === true) {
            _.touchObject.swipeLength = Math.round(Math.sqrt(
                Math.pow(_.touchObject.curY - _.touchObject.startY, 2)));
        }

        swipeDirection = _.swipeDirection();

        if (swipeDirection === 'vertical') {
            return;
        }

        if (event.originalEvent !== undefined && _.touchObject.swipeLength > 4) {
            event.preventDefault();
        }

        positionOffset = (_.options.rtl === false ? 1 : -1) * (_.touchObject.curX > _.touchObject.startX ? 1 : -1);
        if (_.options.verticalSwiping === true) {
            positionOffset = _.touchObject.curY > _.touchObject.startY ? 1 : -1;
        }


        swipeLength = _.touchObject.swipeLength;

        _.touchObject.edgeHit = false;

        if (_.options.infinite === false) {
            if ((_.currentSlide === 0 && swipeDirection === 'right') || (_.currentSlide >= _.getDotCount() && swipeDirection === 'left')) {
                swipeLength = _.touchObject.swipeLength * _.options.edgeFriction;
                _.touchObject.edgeHit = true;
            }
        }

        if (_.options.vertical === false) {
            _.swipeLeft = curLeft + swipeLength * positionOffset;
        } else {
            _.swipeLeft = curLeft + (swipeLength * (_.$list.height() / _.listWidth)) * positionOffset;
        }
        if (_.options.verticalSwiping === true) {
            _.swipeLeft = curLeft + swipeLength * positionOffset;
        }

        if (_.options.fade === true || _.options.touchMove === false) {
            return false;
        }

        if (_.animating === true) {
            _.swipeLeft = null;
            return false;
        }

        _.setCSS(_.swipeLeft);

    };

    Slick.prototype.swipeStart = function (event) {

        var _ = this,
            touches;

        if (_.touchObject.fingerCount !== 1 || _.slideCount <= _.options.slidesToShow) {
            _.touchObject = {};
            return false;
        }

        if (event.originalEvent !== undefined && event.originalEvent.touches !== undefined) {
            touches = event.originalEvent.touches[0];
        }

        _.touchObject.startX = _.touchObject.curX = touches !== undefined ? touches.pageX : event.clientX;
        _.touchObject.startY = _.touchObject.curY = touches !== undefined ? touches.pageY : event.clientY;

        _.dragging = true;

    };

    Slick.prototype.unfilterSlides = Slick.prototype.slickUnfilter = function () {

        var _ = this;

        if (_.$slidesCache !== null) {

            _.unload();

            _.$slideTrack.children(this.options.slide).detach();

            _.$slidesCache.appendTo(_.$slideTrack);

            _.reinit();

        }

    };

    Slick.prototype.unload = function () {

        var _ = this;

        $('.slick-cloned', _.$slider).remove();

        if (_.$dots) {
            _.$dots.remove();
        }

        if (_.$prevArrow && _.htmlExpr.test(_.options.prevArrow)) {
            _.$prevArrow.remove();
        }

        if (_.$nextArrow && _.htmlExpr.test(_.options.nextArrow)) {
            _.$nextArrow.remove();
        }

        _.$slides
            .removeClass('slick-slide slick-active slick-visible slick-current')
            .attr('aria-hidden', 'true')
            .css('width', '');

    };

    Slick.prototype.unslick = function (fromBreakpoint) {

        var _ = this;
        _.$slider.trigger('unslick', [_, fromBreakpoint]);
        _.destroy();

    };

    Slick.prototype.updateArrows = function () {

        var _ = this,
            centerOffset;

        centerOffset = Math.floor(_.options.slidesToShow / 2);

        if (_.options.arrows === true &&
            _.slideCount > _.options.slidesToShow &&
            !_.options.infinite) {

            _.$prevArrow.removeClass('slick-disabled').attr('aria-disabled', 'false');
            _.$nextArrow.removeClass('slick-disabled').attr('aria-disabled', 'false');

            if (_.currentSlide === 0) {

                _.$prevArrow.addClass('slick-disabled').attr('aria-disabled', 'true');
                _.$nextArrow.removeClass('slick-disabled').attr('aria-disabled', 'false');

            } else if (_.currentSlide >= _.slideCount - _.options.slidesToShow && _.options.centerMode === false) {

                _.$nextArrow.addClass('slick-disabled').attr('aria-disabled', 'true');
                _.$prevArrow.removeClass('slick-disabled').attr('aria-disabled', 'false');

            } else if (_.currentSlide >= _.slideCount - 1 && _.options.centerMode === true) {

                _.$nextArrow.addClass('slick-disabled').attr('aria-disabled', 'true');
                _.$prevArrow.removeClass('slick-disabled').attr('aria-disabled', 'false');

            }

        }

    };

    Slick.prototype.updateDots = function () {

        var _ = this;

        if (_.$dots !== null) {

            _.$dots
                .find('li')
                .removeClass('slick-active')
                .attr('aria-hidden', 'true');

            _.$dots
                .find('li')
                .eq(Math.floor(_.currentSlide / _.options.slidesToScroll))
                .addClass('slick-active')
                .attr('aria-hidden', 'false');

        }

    };

    Slick.prototype.visibility = function () {

        var _ = this;

        if (document[_.hidden]) {
            _.paused = true;
            _.autoPlayClear();
        } else {
            if (_.options.autoplay === true) {
                _.paused = false;
                _.autoPlay();
            }
        }

    };
    Slick.prototype.initADA = function () {
        var _ = this;
        _.$slides.add(_.$slideTrack.find('.slick-cloned')).attr({
            'aria-hidden': 'true',
            'tabindex': '-1'
        }).find('a, input, button, select').attr({
            'tabindex': '-1'
        });

        _.$slideTrack.attr('role', 'listbox');

        _.$slides.not(_.$slideTrack.find('.slick-cloned')).each(function (i) {
            $(this).attr({
                'role': 'option',
                'aria-describedby': 'slick-slide' + _.instanceUid + i + ''
            });
        });

        if (_.$dots !== null) {
            _.$dots.attr('role', 'tablist').find('li').each(function (i) {
                $(this).attr({
                    'role': 'presentation',
                    'aria-selected': 'false',
                    'aria-controls': 'navigation' + _.instanceUid + i + '',
                    'id': 'slick-slide' + _.instanceUid + i + ''
                });
            })
                .first().attr('aria-selected', 'true').end()
                .find('button').attr('role', 'button').end()
                .closest('div').attr('role', 'toolbar');
        }
        _.activateADA();

    };

    Slick.prototype.activateADA = function () {
        var _ = this;

        _.$slideTrack.find('.slick-active').attr({
            'aria-hidden': 'false'
        }).find('a, input, button, select').attr({
            'tabindex': '0'
        });

    };

    Slick.prototype.focusHandler = function () {
        var _ = this;
        _.$slider.on('focus.slick blur.slick', '*', function (event) {
            event.stopImmediatePropagation();
            var sf = $(this);
            setTimeout(function () {
                if (_.isPlay) {
                    if (sf.is(':focus')) {
                        _.autoPlayClear();
                        _.paused = true;
                    } else {
                        _.paused = false;
                        _.autoPlay();
                    }
                }
            }, 0);
        });
    };

    $.fn.slick = function () {
        var _ = this,
            opt = arguments[0],
            args = Array.prototype.slice.call(arguments, 1),
            l = _.length,
            i,
            ret;
        for (i = 0; i < l; i++) {
            if (typeof opt == 'object' || typeof opt == 'undefined')
                _[i].slick = new Slick(_[i], opt);
            else
                ret = _[i].slick[opt].apply(_[i].slick, args);
            if (typeof ret != 'undefined') return ret;
        }
        return _;
    };

}));


/**
  * Template Name: Varsity
  * Version: 1.0  
  * Template Scripts
  * Author: MarkUps
  * Author URI: http://www.markups.io/

  Custom JS
  

/*
jQuery Waypoints - v2.0.3
Copyright (c) 2011-2013 Caleb Troughton
Dual licensed under the MIT license and GPL license.
https://github.com/imakewebthings/jquery-waypoints/blob/master/licenses.txt
*/
(function () {
    var t = [].indexOf || function (t) {
        for (var e = 0, n = this.length; n > e; e++) if (e in this && this[e] === t) return e;
        return -1;
    }, e = [].slice;
    (function (t, e) {
        return "function" == typeof define && define.amd ? define("waypoints", ["jquery"], function (n) {
            return e(n, t);
        }) : e(t.jQuery, t);
    })(this, function (n, r) {
        var o, i, l, s, c, a, u, f, h, d, p, v, y, w, g, m;
        return o = n(r), f = t.call(r, "ontouchstart") >= 0, s = {
            horizontal: {},
            vertical: {}
        }, c = 1, u = {}, a = "waypoints-context-id", p = "resize.waypoints", v = "scroll.waypoints",
            y = 1, w = "waypoints-waypoint-ids", g = "waypoint", m = "waypoints", i = function () {
                function t(t) {
                    var e = this;
                    this.$element = t, this.element = t[0], this.didResize = !1, this.didScroll = !1,
                        this.id = "context" + c++ , this.oldScroll = {
                            x: t.scrollLeft(),
                            y: t.scrollTop()
                        }, this.waypoints = {
                            horizontal: {},
                            vertical: {}
                        }, t.data(a, this.id), u[this.id] = this, t.bind(v, function () {
                            var t;
                            return e.didScroll || f ? void 0 : (e.didScroll = !0, t = function () {
                                return e.doScroll(), e.didScroll = !1;
                            }, r.setTimeout(t, n[m].settings.scrollThrottle));
                        }), t.bind(p, function () {
                            var t;
                            return e.didResize ? void 0 : (e.didResize = !0, t = function () {
                                return n[m]("refresh"), e.didResize = !1;
                            }, r.setTimeout(t, n[m].settings.resizeThrottle));
                        });
                }
                return t.prototype.doScroll = function () {
                    var t, e = this;
                    return t = {
                        horizontal: {
                            newScroll: this.$element.scrollLeft(),
                            oldScroll: this.oldScroll.x,
                            forward: "right",
                            backward: "left"
                        },
                        vertical: {
                            newScroll: this.$element.scrollTop(),
                            oldScroll: this.oldScroll.y,
                            forward: "down",
                            backward: "up"
                        }
                    }, !f || t.vertical.oldScroll && t.vertical.newScroll || n[m]("refresh"), n.each(t, function (t, r) {
                        var o, i, l;
                        return l = [], i = r.newScroll > r.oldScroll, o = i ? r.forward : r.backward, n.each(e.waypoints[t], function (t, e) {
                            var n, o;
                            return r.oldScroll < (n = e.offset) && n <= r.newScroll ? l.push(e) : r.newScroll < (o = e.offset) && o <= r.oldScroll ? l.push(e) : void 0;
                        }), l.sort(function (t, e) {
                            return t.offset - e.offset;
                        }), i || l.reverse(), n.each(l, function (t, e) {
                            return e.options.continuous || t === l.length - 1 ? e.trigger([o]) : void 0;
                        });
                    }), this.oldScroll = {
                        x: t.horizontal.newScroll,
                        y: t.vertical.newScroll
                    };
                }, t.prototype.refresh = function () {
                    var t, e, r, o = this;
                    return r = n.isWindow(this.element), e = this.$element.offset(), this.doScroll(),
                        t = {
                            horizontal: {
                                contextOffset: r ? 0 : e.left,
                                contextScroll: r ? 0 : this.oldScroll.x,
                                contextDimension: this.$element.width(),
                                oldScroll: this.oldScroll.x,
                                forward: "right",
                                backward: "left",
                                offsetProp: "left"
                            },
                            vertical: {
                                contextOffset: r ? 0 : e.top,
                                contextScroll: r ? 0 : this.oldScroll.y,
                                contextDimension: r ? n[m]("viewportHeight") : this.$element.height(),
                                oldScroll: this.oldScroll.y,
                                forward: "down",
                                backward: "up",
                                offsetProp: "top"
                            }
                        }, n.each(t, function (t, e) {
                            return n.each(o.waypoints[t], function (t, r) {
                                var o, i, l, s, c;
                                return o = r.options.offset, l = r.offset, i = n.isWindow(r.element) ? 0 : r.$element.offset()[e.offsetProp],
                                    n.isFunction(o) ? o = o.apply(r.element) : "string" == typeof o && (o = parseFloat(o),
                                        r.options.offset.indexOf("%") > -1 && (o = Math.ceil(e.contextDimension * o / 100))),
                                    r.offset = i - e.contextOffset + e.contextScroll - o, r.options.onlyOnScroll && null != l || !r.enabled ? void 0 : null !== l && l < (s = e.oldScroll) && s <= r.offset ? r.trigger([e.backward]) : null !== l && l > (c = e.oldScroll) && c >= r.offset ? r.trigger([e.forward]) : null === l && e.oldScroll >= r.offset ? r.trigger([e.forward]) : void 0;
                            });
                        });
                }, t.prototype.checkEmpty = function () {
                    return n.isEmptyObject(this.waypoints.horizontal) && n.isEmptyObject(this.waypoints.vertical) ? (this.$element.unbind([p, v].join(" ")),
                        delete u[this.id]) : void 0;
                }, t;
            }(), l = function () {
                function t(t, e, r) {
                    var o, i;
                    r = n.extend({}, n.fn[g].defaults, r), "bottom-in-view" === r.offset && (r.offset = function () {
                        var t;
                        return t = n[m]("viewportHeight"), n.isWindow(e.element) || (t = e.$element.height()),
                            t - n(this).outerHeight();
                    }), this.$element = t, this.element = t[0], this.axis = r.horizontal ? "horizontal" : "vertical",
                        this.callback = r.handler, this.context = e, this.enabled = r.enabled, this.id = "waypoints" + y++ ,
                        this.offset = null, this.options = r, e.waypoints[this.axis][this.id] = this, s[this.axis][this.id] = this,
                        o = null != (i = t.data(w)) ? i : [], o.push(this.id), t.data(w, o);
                }
                return t.prototype.trigger = function (t) {
                    return this.enabled ? (null != this.callback && this.callback.apply(this.element, t),
                        this.options.triggerOnce ? this.destroy() : void 0) : void 0;
                }, t.prototype.disable = function () {
                    return this.enabled = !1;
                }, t.prototype.enable = function () {
                    return this.context.refresh(), this.enabled = !0;
                }, t.prototype.destroy = function () {
                    return delete s[this.axis][this.id], delete this.context.waypoints[this.axis][this.id],
                        this.context.checkEmpty();
                }, t.getWaypointsByElement = function (t) {
                    var e, r;
                    return (r = n(t).data(w)) ? (e = n.extend({}, s.horizontal, s.vertical), n.map(r, function (t) {
                        return e[t];
                    })) : [];
                }, t;
            }(), d = {
                init: function (t, e) {
                    var r;
                    return null == e && (e = {}), null == (r = e.handler) && (e.handler = t), this.each(function () {
                        var t, r, o, s;
                        return t = n(this), o = null != (s = e.context) ? s : n.fn[g].defaults.context, n.isWindow(o) || (o = t.closest(o)),
                            o = n(o), r = u[o.data(a)], r || (r = new i(o)), new l(t, r, e);
                    }), n[m]("refresh"), this;
                },
                disable: function () {
                    return d._invoke(this, "disable");
                },
                enable: function () {
                    return d._invoke(this, "enable");
                },
                destroy: function () {
                    return d._invoke(this, "destroy");
                },
                prev: function (t, e) {
                    return d._traverse.call(this, t, e, function (t, e, n) {
                        return e > 0 ? t.push(n[e - 1]) : void 0;
                    });
                },
                next: function (t, e) {
                    return d._traverse.call(this, t, e, function (t, e, n) {
                        return e < n.length - 1 ? t.push(n[e + 1]) : void 0;
                    });
                },
                _traverse: function (t, e, o) {
                    var i, l;
                    return null == t && (t = "vertical"), null == e && (e = r), l = h.aggregate(e),
                        i = [], this.each(function () {
                            var e;
                            return e = n.inArray(this, l[t]), o(i, e, l[t]);
                        }), this.pushStack(i);
                },
                _invoke: function (t, e) {
                    return t.each(function () {
                        var t;
                        return t = l.getWaypointsByElement(this), n.each(t, function (t, n) {
                            return n[e](), !0;
                        });
                    }), this;
                }
            }, n.fn[g] = function () {
                var t, r;
                return r = arguments[0], t = 2 <= arguments.length ? e.call(arguments, 1) : [], d[r] ? d[r].apply(this, t) : n.isFunction(r) ? d.init.apply(this, arguments) : n.isPlainObject(r) ? d.init.apply(this, [null, r]) : r ? n.error("The " + r + " method does not exist in jQuery Waypoints.") : n.error("jQuery Waypoints needs a callback function or handler option.");
            }, n.fn[g].defaults = {
                context: r,
                continuous: !0,
                enabled: !0,
                horizontal: !1,
                offset: 0,
                triggerOnce: !1
            }, h = {
                refresh: function () {
                    return n.each(u, function (t, e) {
                        return e.refresh();
                    });
                },
                viewportHeight: function () {
                    var t;
                    return null != (t = r.innerHeight) ? t : o.height();
                },
                aggregate: function (t) {
                    var e, r, o;
                    return e = s, t && (e = null != (o = u[n(t).data(a)]) ? o.waypoints : void 0), e ? (r = {
                        horizontal: [],
                        vertical: []
                    }, n.each(r, function (t, o) {
                        return n.each(e[t], function (t, e) {
                            return o.push(e);
                        }), o.sort(function (t, e) {
                            return t.offset - e.offset;
                        }), r[t] = n.map(o, function (t) {
                            return t.element;
                        }), r[t] = n.unique(r[t]);
                    }), r) : [];
                },
                above: function (t) {
                    return null == t && (t = r), h._filter(t, "vertical", function (t, e) {
                        return e.offset <= t.oldScroll.y;
                    });
                },
                below: function (t) {
                    return null == t && (t = r), h._filter(t, "vertical", function (t, e) {
                        return e.offset > t.oldScroll.y;
                    });
                },
                left: function (t) {
                    return null == t && (t = r), h._filter(t, "horizontal", function (t, e) {
                        return e.offset <= t.oldScroll.x;
                    });
                },
                right: function (t) {
                    return null == t && (t = r), h._filter(t, "horizontal", function (t, e) {
                        return e.offset > t.oldScroll.x;
                    });
                },
                enable: function () {
                    return h._invoke("enable");
                },
                disable: function () {
                    return h._invoke("disable");
                },
                destroy: function () {
                    return h._invoke("destroy");
                },
                extendFn: function (t, e) {
                    return d[t] = e;
                },
                _invoke: function (t) {
                    var e;
                    return e = n.extend({}, s.vertical, s.horizontal), n.each(e, function (e, n) {
                        return n[t](), !0;
                    });
                },
                _filter: function (t, e, r) {
                    var o, i;
                    return (o = u[n(t).data(a)]) ? (i = [], n.each(o.waypoints[e], function (t, e) {
                        return r(o, e) ? i.push(e) : void 0;
                    }), i.sort(function (t, e) {
                        return t.offset - e.offset;
                    }), n.map(i, function (t) {
                        return t.element;
                    })) : [];
                }
            }, n[m] = function () {
                var t, n;
                return n = arguments[0], t = 2 <= arguments.length ? e.call(arguments, 1) : [], h[n] ? h[n].apply(null, t) : h.aggregate.call(null, n);
            }, n[m].settings = {
                resizeThrottle: 100,
                scrollThrottle: 30
            }, o.load(function () {
                return n[m]("refresh");
            });
    });
}).call(this);


//counterup
/*!
* jquery.counterup.js 1.0
*
* Copyright 2013, Benjamin Intal http://gambit.ph @bfintal
* Released under the GPL v2 License
*
* Date: Nov 26, 2013
*/
(function ($) {
    "use strict";

    $.fn.counterUp = function (options) {

        // Defaults
        var settings = $.extend({
            'time': 400,
            'delay': 10
        }, options);

        return this.each(function () {

            // Store the object
            var $this = $(this);
            var $settings = settings;

            var counterUpper = function () {
                var nums = [];
                var divisions = $settings.time / $settings.delay;
                var num = $this.text();
                var isComma = /[0-9]+,[0-9]+/.test(num);
                num = num.replace(/,/g, '');
                var isInt = /^[0-9]+$/.test(num);
                var isFloat = /^[0-9]+\.[0-9]+$/.test(num);
                var decimalPlaces = isFloat ? (num.split('.')[1] || []).length : 0;

                // Generate list of incremental numbers to display
                for (var i = divisions; i >= 1; i--) {

                    // Preserve as int if input was int
                    var newNum = parseInt(num / divisions * i);

                    // Preserve float if input was float
                    if (isFloat) {
                        newNum = parseFloat(num / divisions * i).toFixed(decimalPlaces);
                    }

                    // Preserve commas if input had commas
                    if (isComma) {
                        while (/(\d+)(\d{3})/.test(newNum.toString())) {
                            newNum = newNum.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
                        }
                    }

                    nums.unshift(newNum);
                }

                $this.data('counterup-nums', nums);
                $this.text('0');

                // Updates the number until we're done
                var f = function () {
                    $this.text($this.data('counterup-nums').shift());
                    if ($this.data('counterup-nums').length) {
                        setTimeout($this.data('counterup-func'), $settings.delay);
                    } else {
                        delete $this.data('counterup-nums');
                        $this.data('counterup-nums', null);
                        $this.data('counterup-func', null);
                    }
                };
                $this.data('counterup-func', f);

                // Start the count up
                setTimeout($this.data('counterup-func'), $settings.delay);
            };

            // Perform counts when the element gets into view
            $this.waypoint(counterUpper, { offset: '100%', triggerOnce: true });
        });

    };

})(jQuery);


//mixitup

/**!
 * MixItUp v2.1.10
 *
 * @copyright Copyright 2015 KunkaLabs Limited.
 * @author    KunkaLabs Limited.
 * @link      https://mixitup.kunkalabs.com
 *
 * @license   Commercial use requires a commercial license.
 *            https://mixitup.kunkalabs.com/licenses/
 *
 *            Non-commercial use permitted under terms of CC-BY-NC license.
 *            http://creativecommons.org/licenses/by-nc/3.0/
 */

(function ($, undf) {
    'use strict';

    /**
     * MixItUp Constructor Function
     * @constructor
     * @extends jQuery
     */

    $.MixItUp = function () {
        var self = this;

        self._execAction('_constructor', 0);

        $.extend(self, {

            /* Public Properties
            ---------------------------------------------------------------------- */

            selectors: {
                target: '.mix',
                filter: '.filter',
                sort: '.sort'
            },

            animation: {
                enable: true,
                effects: 'fade scale',
                duration: 600,
                easing: 'ease',
                perspectiveDistance: '3000',
                perspectiveOrigin: '50% 50%',
                queue: true,
                queueLimit: 1,
                animateChangeLayout: false,
                animateResizeContainer: true,
                animateResizeTargets: false,
                staggerSequence: false,
                reverseOut: false
            },

            callbacks: {
                onMixLoad: false,
                onMixStart: false,
                onMixBusy: false,
                onMixEnd: false,
                onMixFail: false,
                _user: false
            },

            controls: {
                enable: true,
                live: false,
                toggleFilterButtons: false,
                toggleLogic: 'or',
                activeClass: 'active'
            },

            layout: {
                display: 'inline-block',
                containerClass: '',
                containerClassFail: 'fail'
            },

            load: {
                filter: 'all',
                sort: false
            },

            /* Private Properties
            ---------------------------------------------------------------------- */

            _$body: null,
            _$container: null,
            _$targets: null,
            _$parent: null,
            _$sortButtons: null,
            _$filterButtons: null,

            _suckMode: false,
            _mixing: false,
            _sorting: false,
            _clicking: false,
            _loading: true,
            _changingLayout: false,
            _changingClass: false,
            _changingDisplay: false,

            _origOrder: [],
            _startOrder: [],
            _newOrder: [],
            _activeFilter: null,
            _toggleArray: [],
            _toggleString: '',
            _activeSort: 'default:asc',
            _newSort: null,
            _startHeight: null,
            _newHeight: null,
            _incPadding: true,
            _newDisplay: null,
            _newClass: null,
            _targetsBound: 0,
            _targetsDone: 0,
            _queue: [],

            _$show: $(),
            _$hide: $()
        });

        self._execAction('_constructor', 1);
    };

    /**
     * MixItUp Prototype
     * @override
     */

    $.MixItUp.prototype = {
        constructor: $.MixItUp,

        /* Static Properties
        ---------------------------------------------------------------------- */

        _instances: {},
        _handled: {
            _filter: {},
            _sort: {}
        },
        _bound: {
            _filter: {},
            _sort: {}
        },
        _actions: {},
        _filters: {},

        /* Static Methods
        ---------------------------------------------------------------------- */

        /**
         * Extend
         * @since 2.1.0
         * @param {object} new properties/methods
         * @extends {object} prototype
         */

        extend: function (extension) {
            for (var key in extension) {
                $.MixItUp.prototype[key] = extension[key];
            }
        },

        /**
         * Add Action
         * @since 2.1.0
         * @param {string} hook name
         * @param {string} namespace
         * @param {function} function to execute
         * @param {number} priority
         * @extends {object} $.MixItUp.prototype._actions
         */

        addAction: function (hook, name, func, priority) {
            $.MixItUp.prototype._addHook('_actions', hook, name, func, priority);
        },

        /**
         * Add Filter
         * @since 2.1.0
         * @param {string} hook name
         * @param {string} namespace
         * @param {function} function to execute
         * @param {number} priority
         * @extends {object} $.MixItUp.prototype._filters
         */

        addFilter: function (hook, name, func, priority) {
            $.MixItUp.prototype._addHook('_filters', hook, name, func, priority);
        },

        /**
         * Add Hook
         * @since 2.1.0
         * @param {string} type of hook
         * @param {string} hook name
         * @param {function} function to execute
         * @param {number} priority
         * @extends {object} $.MixItUp.prototype._filters
         */

        _addHook: function (type, hook, name, func, priority) {
            var collection = $.MixItUp.prototype[type],
                obj = {};

            priority = (priority === 1 || priority === 'post') ? 'post' : 'pre';

            obj[hook] = {};
            obj[hook][priority] = {};
            obj[hook][priority][name] = func;

            $.extend(true, collection, obj);
        },


        /* Private Methods
        ---------------------------------------------------------------------- */

        /**
         * Initialise
         * @since 2.0.0
         * @param {object} domNode
         * @param {object} config
         */

        _init: function (domNode, config) {
            var self = this;

            self._execAction('_init', 0, arguments);

            config && $.extend(true, self, config);

            self._$body = $('body');
            self._domNode = domNode;
            self._$container = $(domNode);
            self._$container.addClass(self.layout.containerClass);
            self._id = domNode.id;

            self._platformDetect();

            self._brake = self._getPrefixedCSS('transition', 'none');

            self._refresh(true);

            self._$parent = self._$targets.parent().length ? self._$targets.parent() : self._$container;

            if (self.load.sort) {
                self._newSort = self._parseSort(self.load.sort);
                self._newSortString = self.load.sort;
                self._activeSort = self.load.sort;
                self._sort();
                self._printSort();
            }

            self._activeFilter = self.load.filter === 'all' ?
                self.selectors.target :
                self.load.filter === 'none' ?
                    '' :
                    self.load.filter;

            self.controls.enable && self._bindHandlers();

            if (self.controls.toggleFilterButtons) {
                self._buildToggleArray();

                for (var i = 0; i < self._toggleArray.length; i++) {
                    self._updateControls({ filter: self._toggleArray[i], sort: self._activeSort }, true);
                };
            } else if (self.controls.enable) {
                self._updateControls({ filter: self._activeFilter, sort: self._activeSort });
            }

            self._filter();

            self._init = true;

            self._$container.data('mixItUp', self);

            self._execAction('_init', 1, arguments);

            self._buildState();

            self._$targets.css(self._brake);

            self._goMix(self.animation.enable);
        },

        /**
         * Platform Detect
         * @since 2.0.0
         */

        _platformDetect: function () {
            var self = this,
                vendorsTrans = ['Webkit', 'Moz', 'O', 'ms'],
                vendorsRAF = ['webkit', 'moz'],
                chrome = window.navigator.appVersion.match(/Chrome\/(\d+)\./) || false,
                ff = typeof InstallTrigger !== 'undefined',
                prefix = function (el) {
                    for (var i = 0; i < vendorsTrans.length; i++) {
                        if (vendorsTrans[i] + 'Transition' in el.style) {
                            return {
                                prefix: '-' + vendorsTrans[i].toLowerCase() + '-',
                                vendor: vendorsTrans[i]
                            };
                        };
                    };
                    return 'transition' in el.style ? '' : false;
                },
                transPrefix = prefix(self._domNode);

            self._execAction('_platformDetect', 0);

            self._chrome = chrome ? parseInt(chrome[1], 10) : false;
            self._ff = ff ? parseInt(window.navigator.userAgent.match(/rv:([^)]+)\)/)[1]) : false;
            self._prefix = transPrefix.prefix;
            self._vendor = transPrefix.vendor;
            self._suckMode = window.atob && self._prefix ? false : true;

            self._suckMode && (self.animation.enable = false);
            (self._ff && self._ff <= 4) && (self.animation.enable = false);

            /* Polyfills
            ---------------------------------------------------------------------- */

            /**
             * window.requestAnimationFrame
             */

            for (var x = 0; x < vendorsRAF.length && !window.requestAnimationFrame; x++) {
                window.requestAnimationFrame = window[vendorsRAF[x] + 'RequestAnimationFrame'];
            }

            /**
             * Object.getPrototypeOf
             */

            if (typeof Object.getPrototypeOf !== 'function') {
                if (typeof 'test'.__proto__ === 'object') {
                    Object.getPrototypeOf = function (object) {
                        return object.__proto__;
                    };
                } else {
                    Object.getPrototypeOf = function (object) {
                        return object.constructor.prototype;
                    };
                }
            }

            /**
             * Element.nextElementSibling
             */

            if (self._domNode.nextElementSibling === undf) {
                Object.defineProperty(Element.prototype, 'nextElementSibling', {
                    get: function () {
                        var el = this.nextSibling;

                        while (el) {
                            if (el.nodeType === 1) {
                                return el;
                            }
                            el = el.nextSibling;
                        }
                        return null;
                    }
                });
            }

            self._execAction('_platformDetect', 1);
        },

        /**
         * Refresh
         * @since 2.0.0
         * @param {boolean} init
         * @param {boolean} force
         */

        _refresh: function (init, force) {
            var self = this;

            self._execAction('_refresh', 0, arguments);

            self._$targets = self._$container.find(self.selectors.target);

            for (var i = 0; i < self._$targets.length; i++) {
                var target = self._$targets[i];

                if (target.dataset === undf || force) {

                    target.dataset = {};

                    for (var j = 0; j < target.attributes.length; j++) {

                        var attr = target.attributes[j],
                            name = attr.name,
                            val = attr.value;

                        if (name.indexOf('data-') > -1) {
                            var dataName = self._helpers._camelCase(name.substring(5, name.length));
                            target.dataset[dataName] = val;
                        }
                    }
                }

                if (target.mixParent === undf) {
                    target.mixParent = self._id;
                }
            }

            if (
                (self._$targets.length && init) ||
                (!self._origOrder.length && self._$targets.length)
            ) {
                self._origOrder = [];

                for (var i = 0; i < self._$targets.length; i++) {
                    var target = self._$targets[i];

                    self._origOrder.push(target);
                }
            }

            self._execAction('_refresh', 1, arguments);
        },

        /**
         * Bind Handlers
         * @since 2.0.0
         */

        _bindHandlers: function () {
            var self = this,
                filters = $.MixItUp.prototype._bound._filter,
                sorts = $.MixItUp.prototype._bound._sort;

            self._execAction('_bindHandlers', 0);

            if (self.controls.live) {
                self._$body
                    .on('click.mixItUp.' + self._id, self.selectors.sort, function () {
                        self._processClick($(this), 'sort');
                    })
                    .on('click.mixItUp.' + self._id, self.selectors.filter, function () {
                        self._processClick($(this), 'filter');
                    });
            } else {
                self._$sortButtons = $(self.selectors.sort);
                self._$filterButtons = $(self.selectors.filter);

                self._$sortButtons.on('click.mixItUp.' + self._id, function () {
                    self._processClick($(this), 'sort');
                });

                self._$filterButtons.on('click.mixItUp.' + self._id, function () {
                    self._processClick($(this), 'filter');
                });
            }

            filters[self.selectors.filter] = (filters[self.selectors.filter] === undf) ? 1 : filters[self.selectors.filter] + 1;
            sorts[self.selectors.sort] = (sorts[self.selectors.sort] === undf) ? 1 : sorts[self.selectors.sort] + 1;

            self._execAction('_bindHandlers', 1);
        },

        /**
         * Process Click
         * @since 2.0.0
         * @param {object} $button
         * @param {string} type
         */

        _processClick: function ($button, type) {
            var self = this,
                trackClick = function ($button, type, off) {
                    var proto = $.MixItUp.prototype;

                    proto._handled['_' + type][self.selectors[type]] = (proto._handled['_' + type][self.selectors[type]] === undf) ?
                        1 :
                        proto._handled['_' + type][self.selectors[type]] + 1;

                    if (proto._handled['_' + type][self.selectors[type]] === proto._bound['_' + type][self.selectors[type]]) {
                        $button[(off ? 'remove' : 'add') + 'Class'](self.controls.activeClass);
                        delete proto._handled['_' + type][self.selectors[type]];
                    }
                };

            self._execAction('_processClick', 0, arguments);

            if (!self._mixing || (self.animation.queue && self._queue.length < self.animation.queueLimit)) {
                self._clicking = true;

                if (type === 'sort') {
                    var sort = $button.attr('data-sort');

                    if (!$button.hasClass(self.controls.activeClass) || sort.indexOf('random') > -1) {
                        $(self.selectors.sort).removeClass(self.controls.activeClass);
                        trackClick($button, type);
                        self.sort(sort);
                    }
                }

                if (type === 'filter') {
                    var filter = $button.attr('data-filter'),
                        ndx,
                        seperator = self.controls.toggleLogic === 'or' ? ',' : '';

                    if (!self.controls.toggleFilterButtons) {
                        if (!$button.hasClass(self.controls.activeClass)) {
                            $(self.selectors.filter).removeClass(self.controls.activeClass);
                            trackClick($button, type);
                            self.filter(filter);
                        }
                    } else {
                        self._buildToggleArray();

                        if (!$button.hasClass(self.controls.activeClass)) {
                            trackClick($button, type);

                            self._toggleArray.push(filter);
                        } else {
                            trackClick($button, type, true);
                            ndx = self._toggleArray.indexOf(filter);
                            self._toggleArray.splice(ndx, 1);
                        }

                        self._toggleArray = $.grep(self._toggleArray, function (n) { return (n); });

                        self._toggleString = self._toggleArray.join(seperator);

                        self.filter(self._toggleString);
                    }
                }

                self._execAction('_processClick', 1, arguments);
            } else {
                if (typeof self.callbacks.onMixBusy === 'function') {
                    self.callbacks.onMixBusy.call(self._domNode, self._state, self);
                }
                self._execAction('_processClickBusy', 1, arguments);
            }
        },

        /**
         * Build Toggle Array
         * @since 2.0.0
         */

        _buildToggleArray: function () {
            var self = this,
                activeFilter = self._activeFilter.replace(/\s/g, '');

            self._execAction('_buildToggleArray', 0, arguments);

            if (self.controls.toggleLogic === 'or') {
                self._toggleArray = activeFilter.split(',');
            } else {
                self._toggleArray = activeFilter.split('.');

                !self._toggleArray[0] && self._toggleArray.shift();

                for (var i = 0, filter; filter = self._toggleArray[i]; i++) {
                    self._toggleArray[i] = '.' + filter;
                }
            }

            self._execAction('_buildToggleArray', 1, arguments);
        },

        /**
         * Update Controls
         * @since 2.0.0
         * @param {object} command
         * @param {boolean} multi
         */

        _updateControls: function (command, multi) {
            var self = this,
                output = {
                    filter: command.filter,
                    sort: command.sort
                },
                update = function ($el, filter) {
                    try {
                        (multi && type === 'filter' && !(output.filter === 'none' || output.filter === '')) ?
                            $el.filter(filter).addClass(self.controls.activeClass) :
                            $el.removeClass(self.controls.activeClass).filter(filter).addClass(self.controls.activeClass);
                    } catch (e) { }
                },
                type = 'filter',
                $el = null;

            self._execAction('_updateControls', 0, arguments);

            (command.filter === undf) && (output.filter = self._activeFilter);
            (command.sort === undf) && (output.sort = self._activeSort);
            (output.filter === self.selectors.target) && (output.filter = 'all');

            for (var i = 0; i < 2; i++) {
                $el = self.controls.live ? $(self.selectors[type]) : self['_$' + type + 'Buttons'];
                $el && update($el, '[data-' + type + '="' + output[type] + '"]');
                type = 'sort';
            }

            self._execAction('_updateControls', 1, arguments);
        },

        /**
         * Filter (private)
         * @since 2.0.0
         */

        _filter: function () {
            var self = this;

            self._execAction('_filter', 0);

            for (var i = 0; i < self._$targets.length; i++) {
                var $target = $(self._$targets[i]);

                if ($target.is(self._activeFilter)) {
                    self._$show = self._$show.add($target);
                } else {
                    self._$hide = self._$hide.add($target);
                }
            }

            self._execAction('_filter', 1);
        },

        /**
         * Sort (private)
         * @since 2.0.0
         */

        _sort: function () {
            var self = this,
                arrayShuffle = function (oldArray) {
                    var newArray = oldArray.slice(),
                        len = newArray.length,
                        i = len;

                    while (i--) {
                        var p = parseInt(Math.random() * len);
                        var t = newArray[i];
                        newArray[i] = newArray[p];
                        newArray[p] = t;
                    };
                    return newArray;
                };

            self._execAction('_sort', 0);

            self._startOrder = [];

            for (var i = 0; i < self._$targets.length; i++) {
                var target = self._$targets[i];

                self._startOrder.push(target);
            }

            switch (self._newSort[0].sortBy) {
                case 'default':
                    self._newOrder = self._origOrder;
                    break;
                case 'random':
                    self._newOrder = arrayShuffle(self._startOrder);
                    break;
                case 'custom':
                    self._newOrder = self._newSort[0].order;
                    break;
                default:
                    self._newOrder = self._startOrder.concat().sort(function (a, b) {
                        return self._compare(a, b);
                    });
            }

            self._execAction('_sort', 1);
        },

        /**
         * Compare Algorithm
         * @since 2.0.0
         * @param {string|number} a
         * @param {string|number} b
         * @param {number} depth (recursion)
         * @return {number}
         */

        _compare: function (a, b, depth) {
            depth = depth ? depth : 0;

            var self = this,
                order = self._newSort[depth].order,
                getData = function (el) {
                    return el.dataset[self._newSort[depth].sortBy] || 0;
                },
                attrA = isNaN(getData(a) * 1) ? getData(a).toLowerCase() : getData(a) * 1,
                attrB = isNaN(getData(b) * 1) ? getData(b).toLowerCase() : getData(b) * 1;

            if (attrA < attrB)
                return order === 'asc' ? -1 : 1;
            if (attrA > attrB)
                return order === 'asc' ? 1 : -1;
            if (attrA === attrB && self._newSort.length > depth + 1)
                return self._compare(a, b, depth + 1);

            return 0;
        },

        /**
         * Print Sort
         * @since 2.0.0
         * @param {boolean} reset
         */

        _printSort: function (reset) {
            var self = this,
                order = reset ? self._startOrder : self._newOrder,
                targets = self._$parent[0].querySelectorAll(self.selectors.target),
                nextSibling = targets.length ? targets[targets.length - 1].nextElementSibling : null,
                frag = document.createDocumentFragment();

            self._execAction('_printSort', 0, arguments);

            for (var i = 0; i < targets.length; i++) {
                var target = targets[i],
                    whiteSpace = target.nextSibling;

                if (target.style.position === 'absolute') continue;

                if (whiteSpace && whiteSpace.nodeName === '#text') {
                    self._$parent[0].removeChild(whiteSpace);
                }

                self._$parent[0].removeChild(target);
            }

            for (var i = 0; i < order.length; i++) {
                var el = order[i];

                if (self._newSort[0].sortBy === 'default' && self._newSort[0].order === 'desc' && !reset) {
                    var firstChild = frag.firstChild;
                    frag.insertBefore(el, firstChild);
                    frag.insertBefore(document.createTextNode(' '), el);
                } else {
                    frag.appendChild(el);
                    frag.appendChild(document.createTextNode(' '));
                }
            }

            nextSibling ?
                self._$parent[0].insertBefore(frag, nextSibling) :
                self._$parent[0].appendChild(frag);

            self._execAction('_printSort', 1, arguments);
        },

        /**
         * Parse Sort
         * @since 2.0.0
         * @param {string} sortString
         * @return {array} newSort
         */

        _parseSort: function (sortString) {
            var self = this,
                rules = typeof sortString === 'string' ? sortString.split(' ') : [sortString],
                newSort = [];

            for (var i = 0; i < rules.length; i++) {
                var rule = typeof sortString === 'string' ? rules[i].split(':') : ['custom', rules[i]],
                    ruleObj = {
                        sortBy: self._helpers._camelCase(rule[0]),
                        order: rule[1] || 'asc'
                    };

                newSort.push(ruleObj);

                if (ruleObj.sortBy === 'default' || ruleObj.sortBy === 'random') break;
            }

            return self._execFilter('_parseSort', newSort, arguments);
        },

        /**
         * Parse Effects
         * @since 2.0.0
         * @return {object} effects
         */

        _parseEffects: function () {
            var self = this,
                effects = {
                    opacity: '',
                    transformIn: '',
                    transformOut: '',
                    filter: ''
                },
                parse = function (effect, extract, reverse) {
                    if (self.animation.effects.indexOf(effect) > -1) {
                        if (extract) {
                            var propIndex = self.animation.effects.indexOf(effect + '(');
                            if (propIndex > -1) {
                                var str = self.animation.effects.substring(propIndex),
                                    match = /\(([^)]+)\)/.exec(str),
                                    val = match[1];

                                return { val: val };
                            }
                        }
                        return true;
                    } else {
                        return false;
                    }
                },
                negate = function (value, invert) {
                    if (invert) {
                        return value.charAt(0) === '-' ? value.substr(1, value.length) : '-' + value;
                    } else {
                        return value;
                    }
                },
                buildTransform = function (key, invert) {
                    var transforms = [
                        ['scale', '.01'],
                        ['translateX', '20px'],
                        ['translateY', '20px'],
                        ['translateZ', '20px'],
                        ['rotateX', '90deg'],
                        ['rotateY', '90deg'],
                        ['rotateZ', '180deg'],
                    ];

                    for (var i = 0; i < transforms.length; i++) {
                        var prop = transforms[i][0],
                            def = transforms[i][1],
                            inverted = invert && prop !== 'scale';

                        effects[key] += parse(prop) ? prop + '(' + negate(parse(prop, true).val || def, inverted) + ') ' : '';
                    }
                };

            effects.opacity = parse('fade') ? parse('fade', true).val || '0' : '1';

            buildTransform('transformIn');

            self.animation.reverseOut ? buildTransform('transformOut', true) : (effects.transformOut = effects.transformIn);

            effects.transition = {};

            effects.transition = self._getPrefixedCSS('transition', 'all ' + self.animation.duration + 'ms ' + self.animation.easing + ', opacity ' + self.animation.duration + 'ms linear');

            self.animation.stagger = parse('stagger') ? true : false;
            self.animation.staggerDuration = parseInt(parse('stagger') ? (parse('stagger', true).val ? parse('stagger', true).val : 100) : 100);

            return self._execFilter('_parseEffects', effects);
        },

        /**
         * Build State
         * @since 2.0.0
         * @param {boolean} future
         * @return {object} futureState
         */

        _buildState: function (future) {
            var self = this,
                state = {};

            self._execAction('_buildState', 0);

            state = {
                activeFilter: self._activeFilter === '' ? 'none' : self._activeFilter,
                activeSort: future && self._newSortString ? self._newSortString : self._activeSort,
                fail: !self._$show.length && self._activeFilter !== '',
                $targets: self._$targets,
                $show: self._$show,
                $hide: self._$hide,
                totalTargets: self._$targets.length,
                totalShow: self._$show.length,
                totalHide: self._$hide.length,
                display: future && self._newDisplay ? self._newDisplay : self.layout.display
            };

            if (future) {
                return self._execFilter('_buildState', state);
            } else {
                self._state = state;

                self._execAction('_buildState', 1);
            }
        },

        /**
         * Go Mix
         * @since 2.0.0
         * @param {boolean} animate
         */

        _goMix: function (animate) {
            var self = this,
                phase1 = function () {
                    if (self._chrome && (self._chrome === 31)) {
                        chromeFix(self._$parent[0]);
                    }

                    self._setInter();

                    phase2();
                },
                phase2 = function () {
                    var scrollTop = window.pageYOffset,
                        scrollLeft = window.pageXOffset,
                        docHeight = document.documentElement.scrollHeight;

                    self._getInterMixData();

                    self._setFinal();

                    self._getFinalMixData();

                    (window.pageYOffset !== scrollTop) && window.scrollTo(scrollLeft, scrollTop);

                    self._prepTargets();

                    if (window.requestAnimationFrame) {
                        requestAnimationFrame(phase3);
                    } else {
                        setTimeout(function () {
                            phase3();
                        }, 20);
                    }
                },
                phase3 = function () {
                    self._animateTargets();

                    if (self._targetsBound === 0) {
                        self._cleanUp();
                    }
                },
                chromeFix = function (grid) {
                    var parent = grid.parentElement,
                        placeholder = document.createElement('div'),
                        frag = document.createDocumentFragment();

                    parent.insertBefore(placeholder, grid);
                    frag.appendChild(grid);
                    parent.replaceChild(grid, placeholder);
                },
                futureState = self._buildState(true);

            self._execAction('_goMix', 0, arguments);

            !self.animation.duration && (animate = false);

            self._mixing = true;

            self._$container.removeClass(self.layout.containerClassFail);

            if (typeof self.callbacks.onMixStart === 'function') {
                self.callbacks.onMixStart.call(self._domNode, self._state, futureState, self);
            }

            self._$container.trigger('mixStart', [self._state, futureState, self]);

            self._getOrigMixData();

            if (animate && !self._suckMode) {

                window.requestAnimationFrame ?
                    requestAnimationFrame(phase1) :
                    phase1();

            } else {
                self._cleanUp();
            }

            self._execAction('_goMix', 1, arguments);
        },

        /**
         * Get Target Data
         * @since 2.0.0
         */

        _getTargetData: function (el, stage) {
            var self = this,
                elStyle;

            el.dataset[stage + 'PosX'] = el.offsetLeft;
            el.dataset[stage + 'PosY'] = el.offsetTop;

            if (self.animation.animateResizeTargets) {
                elStyle = !self._suckMode ?
                    window.getComputedStyle(el) :
                    {
                        marginBottom: '',
                        marginRight: ''
                    };

                el.dataset[stage + 'MarginBottom'] = parseInt(elStyle.marginBottom);
                el.dataset[stage + 'MarginRight'] = parseInt(elStyle.marginRight);
                el.dataset[stage + 'Width'] = el.offsetWidth;
                el.dataset[stage + 'Height'] = el.offsetHeight;
            }
        },

        /**
         * Get Original Mix Data
         * @since 2.0.0
         */

        _getOrigMixData: function () {
            var self = this,
                parentStyle = !self._suckMode ? window.getComputedStyle(self._$parent[0]) : { boxSizing: '' },
                parentBS = parentStyle.boxSizing || parentStyle[self._vendor + 'BoxSizing'];

            self._incPadding = (parentBS === 'border-box');

            self._execAction('_getOrigMixData', 0);

            !self._suckMode && (self.effects = self._parseEffects());

            self._$toHide = self._$hide.filter(':visible');
            self._$toShow = self._$show.filter(':hidden');
            self._$pre = self._$targets.filter(':visible');

            self._startHeight = self._incPadding ?
                self._$parent.outerHeight() :
                self._$parent.height();

            for (var i = 0; i < self._$pre.length; i++) {
                var el = self._$pre[i];

                self._getTargetData(el, 'orig');
            }

            self._execAction('_getOrigMixData', 1);
        },

        /**
         * Set Intermediate Positions
         * @since 2.0.0
         */

        _setInter: function () {
            var self = this;

            self._execAction('_setInter', 0);

            if (self._changingLayout && self.animation.animateChangeLayout) {
                self._$toShow.css('display', self._newDisplay);

                if (self._changingClass) {
                    self._$container
                        .removeClass(self.layout.containerClass)
                        .addClass(self._newClass);
                }
            } else {
                self._$toShow.css('display', self.layout.display);
            }

            self._execAction('_setInter', 1);
        },

        /**
         * Get Intermediate Mix Data
         * @since 2.0.0
         */

        _getInterMixData: function () {
            var self = this;

            self._execAction('_getInterMixData', 0);

            for (var i = 0; i < self._$toShow.length; i++) {
                var el = self._$toShow[i];

                self._getTargetData(el, 'inter');
            }

            for (var i = 0; i < self._$pre.length; i++) {
                var el = self._$pre[i];

                self._getTargetData(el, 'inter');
            }

            self._execAction('_getInterMixData', 1);
        },

        /**
         * Set Final Positions
         * @since 2.0.0
         */

        _setFinal: function () {
            var self = this;

            self._execAction('_setFinal', 0);

            self._sorting && self._printSort();

            self._$toHide.removeStyle('display');

            if (self._changingLayout && self.animation.animateChangeLayout) {
                self._$pre.css('display', self._newDisplay);
            }

            self._execAction('_setFinal', 1);
        },

        /**
         * Get Final Mix Data
         * @since 2.0.0
         */

        _getFinalMixData: function () {
            var self = this;

            self._execAction('_getFinalMixData', 0);

            for (var i = 0; i < self._$toShow.length; i++) {
                var el = self._$toShow[i];

                self._getTargetData(el, 'final');
            }

            for (var i = 0; i < self._$pre.length; i++) {
                var el = self._$pre[i];

                self._getTargetData(el, 'final');
            }

            self._newHeight = self._incPadding ?
                self._$parent.outerHeight() :
                self._$parent.height();

            self._sorting && self._printSort(true);

            self._$toShow.removeStyle('display');

            self._$pre.css('display', self.layout.display);

            if (self._changingClass && self.animation.animateChangeLayout) {
                self._$container
                    .removeClass(self._newClass)
                    .addClass(self.layout.containerClass);
            }

            self._execAction('_getFinalMixData', 1);
        },

        /**
         * Prepare Targets
         * @since 2.0.0
         */

        _prepTargets: function () {
            var self = this,
                transformCSS = {
                    _in: self._getPrefixedCSS('transform', self.effects.transformIn),
                    _out: self._getPrefixedCSS('transform', self.effects.transformOut)
                };

            self._execAction('_prepTargets', 0);

            if (self.animation.animateResizeContainer) {
                self._$parent.css('height', self._startHeight + 'px');
            }

            for (var i = 0; i < self._$toShow.length; i++) {
                var el = self._$toShow[i],
                    $el = $(el);

                el.style.opacity = self.effects.opacity;
                el.style.display = (self._changingLayout && self.animation.animateChangeLayout) ?
                    self._newDisplay :
                    self.layout.display;

                $el.css(transformCSS._in);

                if (self.animation.animateResizeTargets) {
                    el.style.width = el.dataset.finalWidth + 'px';
                    el.style.height = el.dataset.finalHeight + 'px';
                    el.style.marginRight = -(el.dataset.finalWidth - el.dataset.interWidth) + (el.dataset.finalMarginRight * 1) + 'px';
                    el.style.marginBottom = -(el.dataset.finalHeight - el.dataset.interHeight) + (el.dataset.finalMarginBottom * 1) + 'px';
                }
            }

            for (var i = 0; i < self._$pre.length; i++) {
                var el = self._$pre[i],
                    $el = $(el),
                    translate = {
                        x: el.dataset.origPosX - el.dataset.interPosX,
                        y: el.dataset.origPosY - el.dataset.interPosY
                    },
                    transformCSS = self._getPrefixedCSS('transform', 'translate(' + translate.x + 'px,' + translate.y + 'px)');

                $el.css(transformCSS);

                if (self.animation.animateResizeTargets) {
                    el.style.width = el.dataset.origWidth + 'px';
                    el.style.height = el.dataset.origHeight + 'px';

                    if (el.dataset.origWidth - el.dataset.finalWidth) {
                        el.style.marginRight = -(el.dataset.origWidth - el.dataset.interWidth) + (el.dataset.origMarginRight * 1) + 'px';
                    }

                    if (el.dataset.origHeight - el.dataset.finalHeight) {
                        el.style.marginBottom = -(el.dataset.origHeight - el.dataset.interHeight) + (el.dataset.origMarginBottom * 1) + 'px';
                    }
                }
            }

            self._execAction('_prepTargets', 1);
        },

        /**
         * Animate Targets
         * @since 2.0.0
         */

        _animateTargets: function () {
            var self = this;

            self._execAction('_animateTargets', 0);

            self._targetsDone = 0;
            self._targetsBound = 0;

            self._$parent
                .css(self._getPrefixedCSS('perspective', self.animation.perspectiveDistance + 'px'))
                .css(self._getPrefixedCSS('perspective-origin', self.animation.perspectiveOrigin));

            if (self.animation.animateResizeContainer) {
                self._$parent
                    .css(self._getPrefixedCSS('transition', 'height ' + self.animation.duration + 'ms ease'))
                    .css('height', self._newHeight + 'px');
            }

            for (var i = 0; i < self._$toShow.length; i++) {
                var el = self._$toShow[i],
                    $el = $(el),
                    translate = {
                        x: el.dataset.finalPosX - el.dataset.interPosX,
                        y: el.dataset.finalPosY - el.dataset.interPosY
                    },
                    delay = self._getDelay(i),
                    toShowCSS = {};

                el.style.opacity = '';

                for (var j = 0; j < 2; j++) {
                    var a = j === 0 ? a = self._prefix : '';

                    if (self._ff && self._ff <= 20) {
                        toShowCSS[a + 'transition-property'] = 'all';
                        toShowCSS[a + 'transition-timing-function'] = self.animation.easing + 'ms';
                        toShowCSS[a + 'transition-duration'] = self.animation.duration + 'ms';
                    }

                    toShowCSS[a + 'transition-delay'] = delay + 'ms';
                    toShowCSS[a + 'transform'] = 'translate(' + translate.x + 'px,' + translate.y + 'px)';
                }

                if (self.effects.transform || self.effects.opacity) {
                    self._bindTargetDone($el);
                }

                (self._ff && self._ff <= 20) ?
                    $el.css(toShowCSS) :
                    $el.css(self.effects.transition).css(toShowCSS);
            }

            for (var i = 0; i < self._$pre.length; i++) {
                var el = self._$pre[i],
                    $el = $(el),
                    translate = {
                        x: el.dataset.finalPosX - el.dataset.interPosX,
                        y: el.dataset.finalPosY - el.dataset.interPosY
                    },
                    delay = self._getDelay(i);

                if (!(
                    el.dataset.finalPosX === el.dataset.origPosX &&
                    el.dataset.finalPosY === el.dataset.origPosY
                )) {
                    self._bindTargetDone($el);
                }

                $el.css(self._getPrefixedCSS('transition', 'all ' + self.animation.duration + 'ms ' + self.animation.easing + ' ' + delay + 'ms'));
                $el.css(self._getPrefixedCSS('transform', 'translate(' + translate.x + 'px,' + translate.y + 'px)'));

                if (self.animation.animateResizeTargets) {
                    if (el.dataset.origWidth - el.dataset.finalWidth && el.dataset.finalWidth * 1) {
                        el.style.width = el.dataset.finalWidth + 'px';
                        el.style.marginRight = -(el.dataset.finalWidth - el.dataset.interWidth) + (el.dataset.finalMarginRight * 1) + 'px';
                    }

                    if (el.dataset.origHeight - el.dataset.finalHeight && el.dataset.finalHeight * 1) {
                        el.style.height = el.dataset.finalHeight + 'px';
                        el.style.marginBottom = -(el.dataset.finalHeight - el.dataset.interHeight) + (el.dataset.finalMarginBottom * 1) + 'px';
                    }
                }
            }

            if (self._changingClass) {
                self._$container
                    .removeClass(self.layout.containerClass)
                    .addClass(self._newClass);
            }

            for (var i = 0; i < self._$toHide.length; i++) {
                var el = self._$toHide[i],
                    $el = $(el),
                    delay = self._getDelay(i),
                    toHideCSS = {};

                for (var j = 0; j < 2; j++) {
                    var a = j === 0 ? a = self._prefix : '';

                    toHideCSS[a + 'transition-delay'] = delay + 'ms';
                    toHideCSS[a + 'transform'] = self.effects.transformOut;
                    toHideCSS.opacity = self.effects.opacity;
                }

                $el.css(self.effects.transition).css(toHideCSS);

                if (self.effects.transform || self.effects.opacity) {
                    self._bindTargetDone($el);
                };
            }

            self._execAction('_animateTargets', 1);

        },

        /**
         * Bind Targets TransitionEnd
         * @since 2.0.0
         * @param {object} $el
         */

        _bindTargetDone: function ($el) {
            var self = this,
                el = $el[0];

            self._execAction('_bindTargetDone', 0, arguments);

            if (!el.dataset.bound) {

                el.dataset.bound = true;
                self._targetsBound++;

                $el.on('webkitTransitionEnd.mixItUp transitionend.mixItUp', function (e) {
                    if (
                        (e.originalEvent.propertyName.indexOf('transform') > -1 ||
                            e.originalEvent.propertyName.indexOf('opacity') > -1) &&
                        $(e.originalEvent.target).is(self.selectors.target)
                    ) {
                        $el.off('.mixItUp');
                        delete el.dataset.bound;
                        self._targetDone();
                    }
                });
            }

            self._execAction('_bindTargetDone', 1, arguments);
        },

        /**
         * Target Done
         * @since 2.0.0
         */

        _targetDone: function () {
            var self = this;

            self._execAction('_targetDone', 0);

            self._targetsDone++;

            (self._targetsDone === self._targetsBound) && self._cleanUp();

            self._execAction('_targetDone', 1);
        },

        /**
         * Clean Up
         * @since 2.0.0
         */

        _cleanUp: function () {
            var self = this,
                targetStyles = self.animation.animateResizeTargets ?
                    'transform opacity width height margin-bottom margin-right' :
                    'transform opacity',
                unBrake = function () {
                    self._$targets.removeStyle('transition', self._prefix);
                };

            self._execAction('_cleanUp', 0);

            !self._changingLayout ?
                self._$show.css('display', self.layout.display) :
                self._$show.css('display', self._newDisplay);

            self._$targets.css(self._brake);

            self._$targets
                .removeStyle(targetStyles, self._prefix)
                .removeAttr('data-inter-pos-x data-inter-pos-y data-final-pos-x data-final-pos-y data-orig-pos-x data-orig-pos-y data-orig-height data-orig-width data-final-height data-final-width data-inter-width data-inter-height data-orig-margin-right data-orig-margin-bottom data-inter-margin-right data-inter-margin-bottom data-final-margin-right data-final-margin-bottom');

            self._$hide.removeStyle('display');

            self._$parent.removeStyle('height transition perspective-distance perspective perspective-origin-x perspective-origin-y perspective-origin perspectiveOrigin', self._prefix);

            if (self._sorting) {
                self._printSort();
                self._activeSort = self._newSortString;
                self._sorting = false;
            }

            if (self._changingLayout) {
                if (self._changingDisplay) {
                    self.layout.display = self._newDisplay;
                    self._changingDisplay = false;
                }

                if (self._changingClass) {
                    self._$parent.removeClass(self.layout.containerClass).addClass(self._newClass);
                    self.layout.containerClass = self._newClass;
                    self._changingClass = false;
                }

                self._changingLayout = false;
            }

            self._refresh();

            self._buildState();

            if (self._state.fail) {
                self._$container.addClass(self.layout.containerClassFail);
            }

            self._$show = $();
            self._$hide = $();

            if (window.requestAnimationFrame) {
                requestAnimationFrame(unBrake);
            }

            self._mixing = false;

            if (typeof self.callbacks._user === 'function') {
                self.callbacks._user.call(self._domNode, self._state, self);
            }

            if (typeof self.callbacks.onMixEnd === 'function') {
                self.callbacks.onMixEnd.call(self._domNode, self._state, self);
            }

            self._$container.trigger('mixEnd', [self._state, self]);

            if (self._state.fail) {
                (typeof self.callbacks.onMixFail === 'function') && self.callbacks.onMixFail.call(self._domNode, self._state, self);
                self._$container.trigger('mixFail', [self._state, self]);
            }

            if (self._loading) {
                (typeof self.callbacks.onMixLoad === 'function') && self.callbacks.onMixLoad.call(self._domNode, self._state, self);
                self._$container.trigger('mixLoad', [self._state, self]);
            }

            if (self._queue.length) {
                self._execAction('_queue', 0);

                self.multiMix(self._queue[0][0], self._queue[0][1], self._queue[0][2]);
                self._queue.splice(0, 1);
            }

            self._execAction('_cleanUp', 1);

            self._loading = false;
        },

        /**
         * Get Prefixed CSS
         * @since 2.0.0
         * @param {string} property
         * @param {string} value
         * @param {boolean} prefixValue
         * @return {object} styles
         */

        _getPrefixedCSS: function (property, value, prefixValue) {
            var self = this,
                styles = {},
                prefix = '',
                i = -1;

            for (i = 0; i < 2; i++) {
                prefix = i === 0 ? self._prefix : '';
                prefixValue ? styles[prefix + property] = prefix + value : styles[prefix + property] = value;
            }

            return self._execFilter('_getPrefixedCSS', styles, arguments);
        },

        /**
         * Get Delay
         * @since 2.0.0
         * @param {number} i
         * @return {number} delay
         */

        _getDelay: function (i) {
            var self = this,
                n = typeof self.animation.staggerSequence === 'function' ? self.animation.staggerSequence.call(self._domNode, i, self._state) : i,
                delay = self.animation.stagger ? n * self.animation.staggerDuration : 0;

            return self._execFilter('_getDelay', delay, arguments);
        },

        /**
         * Parse MultiMix Arguments
         * @since 2.0.0
         * @param {array} args
         * @return {object} output
         */

        _parseMultiMixArgs: function (args) {
            var self = this,
                output = {
                    command: null,
                    animate: self.animation.enable,
                    callback: null
                };

            for (var i = 0; i < args.length; i++) {
                var arg = args[i];

                if (arg !== null) {
                    if (typeof arg === 'object' || typeof arg === 'string') {
                        output.command = arg;
                    } else if (typeof arg === 'boolean') {
                        output.animate = arg;
                    } else if (typeof arg === 'function') {
                        output.callback = arg;
                    }
                }
            }

            return self._execFilter('_parseMultiMixArgs', output, arguments);
        },

        /**
         * Parse Insert Arguments
         * @since 2.0.0
         * @param {array} args
         * @return {object} output
         */

        _parseInsertArgs: function (args) {
            var self = this,
                output = {
                    index: 0,
                    $object: $(),
                    multiMix: { filter: self._state.activeFilter },
                    callback: null
                };

            for (var i = 0; i < args.length; i++) {
                var arg = args[i];

                if (typeof arg === 'number') {
                    output.index = arg;
                } else if (typeof arg === 'object' && arg instanceof $) {
                    output.$object = arg;
                } else if (typeof arg === 'object' && self._helpers._isElement(arg)) {
                    output.$object = $(arg);
                } else if (typeof arg === 'object' && arg !== null) {
                    output.multiMix = arg;
                } else if (typeof arg === 'boolean' && !arg) {
                    output.multiMix = false;
                } else if (typeof arg === 'function') {
                    output.callback = arg;
                }
            }

            return self._execFilter('_parseInsertArgs', output, arguments);
        },

        /**
         * Execute Action
         * @since 2.0.0
         * @param {string} methodName
         * @param {boolean} isPost
         * @param {array} args
         */

        _execAction: function (methodName, isPost, args) {
            var self = this,
                context = isPost ? 'post' : 'pre';

            if (!self._actions.isEmptyObject && self._actions.hasOwnProperty(methodName)) {
                for (var key in self._actions[methodName][context]) {
                    self._actions[methodName][context][key].call(self, args);
                }
            }
        },

        /**
         * Execute Filter
         * @since 2.0.0
         * @param {string} methodName
         * @param {mixed} value
         * @return {mixed} value
         */

        _execFilter: function (methodName, value, args) {
            var self = this;

            if (!self._filters.isEmptyObject && self._filters.hasOwnProperty(methodName)) {
                for (var key in self._filters[methodName]) {
                    return self._filters[methodName][key].call(self, args);
                }
            } else {
                return value;
            }
        },

        /* Helpers
        ---------------------------------------------------------------------- */

        _helpers: {

            /**
             * CamelCase
             * @since 2.0.0
             * @param {string}
             * @return {string}
             */

            _camelCase: function (string) {
                return string.replace(/-([a-z])/g, function (g) {
                    return g[1].toUpperCase();
                });
            },

            /**
             * Is Element
             * @since 2.1.3
             * @param {object} element to test
             * @return {boolean}
             */

            _isElement: function (el) {
                if (window.HTMLElement) {
                    return el instanceof HTMLElement;
                } else {
                    return (
                        el !== null &&
                        el.nodeType === 1 &&
                        el.nodeName === 'string'
                    );
                }
            }
        },

        /* Public Methods
        ---------------------------------------------------------------------- */

        /**
         * Is Mixing
         * @since 2.0.0
         * @return {boolean}
         */

        isMixing: function () {
            var self = this;

            return self._execFilter('isMixing', self._mixing);
        },

        /**
         * Filter (public)
         * @since 2.0.0
         * @param {array} arguments
         */

        filter: function () {
            var self = this,
                args = self._parseMultiMixArgs(arguments);

            self._clicking && (self._toggleString = '');

            self.multiMix({ filter: args.command }, args.animate, args.callback);
        },

        /**
         * Sort (public)
         * @since 2.0.0
         * @param {array} arguments
         */

        sort: function () {
            var self = this,
                args = self._parseMultiMixArgs(arguments);

            self.multiMix({ sort: args.command }, args.animate, args.callback);
        },

        /**
         * Change Layout (public)
         * @since 2.0.0
         * @param {array} arguments
         */

        changeLayout: function () {
            var self = this,
                args = self._parseMultiMixArgs(arguments);

            self.multiMix({ changeLayout: args.command }, args.animate, args.callback);
        },

        /**
         * MultiMix
         * @since 2.0.0
         * @param {array} arguments
         */

        multiMix: function () {
            var self = this,
                args = self._parseMultiMixArgs(arguments);

            self._execAction('multiMix', 0, arguments);

            if (!self._mixing) {
                if (self.controls.enable && !self._clicking) {
                    self.controls.toggleFilterButtons && self._buildToggleArray();
                    self._updateControls(args.command, self.controls.toggleFilterButtons);
                }

                (self._queue.length < 2) && (self._clicking = false);

                delete self.callbacks._user;
                if (args.callback) self.callbacks._user = args.callback;

                var sort = args.command.sort,
                    filter = args.command.filter,
                    changeLayout = args.command.changeLayout;

                self._refresh();

                if (sort) {
                    self._newSort = self._parseSort(sort);
                    self._newSortString = sort;

                    self._sorting = true;
                    self._sort();
                }

                if (filter !== undf) {
                    filter = (filter === 'all') ? self.selectors.target : filter;

                    self._activeFilter = filter;
                }

                self._filter();

                if (changeLayout) {
                    self._newDisplay = (typeof changeLayout === 'string') ? changeLayout : changeLayout.display || self.layout.display;
                    self._newClass = changeLayout.containerClass || '';

                    if (
                        self._newDisplay !== self.layout.display ||
                        self._newClass !== self.layout.containerClass
                    ) {
                        self._changingLayout = true;

                        self._changingClass = (self._newClass !== self.layout.containerClass);
                        self._changingDisplay = (self._newDisplay !== self.layout.display);
                    }
                }

                self._$targets.css(self._brake);

                self._goMix(args.animate ^ self.animation.enable ? args.animate : self.animation.enable);

                self._execAction('multiMix', 1, arguments);

            } else {
                if (self.animation.queue && self._queue.length < self.animation.queueLimit) {
                    self._queue.push(arguments);

                    (self.controls.enable && !self._clicking) && self._updateControls(args.command);

                    self._execAction('multiMixQueue', 1, arguments);

                } else {
                    if (typeof self.callbacks.onMixBusy === 'function') {
                        self.callbacks.onMixBusy.call(self._domNode, self._state, self);
                    }
                    self._$container.trigger('mixBusy', [self._state, self]);

                    self._execAction('multiMixBusy', 1, arguments);
                }
            }
        },

        /**
         * Insert
         * @since 2.0.0
         * @param {array} arguments
         */

        insert: function () {
            var self = this,
                args = self._parseInsertArgs(arguments),
                callback = (typeof args.callback === 'function') ? args.callback : null,
                frag = document.createDocumentFragment(),
                target = (function () {
                    self._refresh();

                    if (self._$targets.length) {
                        return (args.index < self._$targets.length || !self._$targets.length) ?
                            self._$targets[args.index] :
                            self._$targets[self._$targets.length - 1].nextElementSibling;
                    } else {
                        return self._$parent[0].children[0];
                    }
                })();

            self._execAction('insert', 0, arguments);

            if (args.$object) {
                for (var i = 0; i < args.$object.length; i++) {
                    var el = args.$object[i];

                    frag.appendChild(el);
                    frag.appendChild(document.createTextNode(' '));
                }

                self._$parent[0].insertBefore(frag, target);
            }

            self._execAction('insert', 1, arguments);

            if (typeof args.multiMix === 'object') {
                self.multiMix(args.multiMix, callback);
            }
        },

        /**
         * Prepend
         * @since 2.0.0
         * @param {array} arguments
         */

        prepend: function () {
            var self = this,
                args = self._parseInsertArgs(arguments);

            self.insert(0, args.$object, args.multiMix, args.callback);
        },

        /**
         * Append
         * @since 2.0.0
         * @param {array} arguments
         */

        append: function () {
            var self = this,
                args = self._parseInsertArgs(arguments);

            self.insert(self._state.totalTargets, args.$object, args.multiMix, args.callback);
        },

        /**
         * Get Option
         * @since 2.0.0
         * @param {string} string
         * @return {mixed} value
         */

        getOption: function (string) {
            var self = this,
                getProperty = function (obj, prop) {
                    var parts = prop.split('.'),
                        last = parts.pop(),
                        l = parts.length,
                        i = 1,
                        current = parts[0] || prop;

                    while ((obj = obj[current]) && i < l) {
                        current = parts[i];
                        i++;
                    }

                    if (obj !== undf) {
                        return obj[last] !== undf ? obj[last] : obj;
                    }
                };

            return string ? self._execFilter('getOption', getProperty(self, string), arguments) : self;
        },

        /**
         * Set Options
         * @since 2.0.0
         * @param {object} config
         */

        setOptions: function (config) {
            var self = this;

            self._execAction('setOptions', 0, arguments);

            typeof config === 'object' && $.extend(true, self, config);

            self._execAction('setOptions', 1, arguments);
        },

        /**
         * Get State
         * @since 2.0.0
         * @return {object} state
         */

        getState: function () {
            var self = this;

            return self._execFilter('getState', self._state, self);
        },

        /**
         * Force Refresh
         * @since 2.1.2
         */

        forceRefresh: function () {
            var self = this;

            self._refresh(false, true);
        },

        /**
         * Destroy
         * @since 2.0.0
         * @param {boolean} hideAll
         */

        destroy: function (hideAll) {
            var self = this,
                filters = $.MixItUp.prototype._bound._filter,
                sorts = $.MixItUp.prototype._bound._sort;

            self._execAction('destroy', 0, arguments);

            self._$body
                .add($(self.selectors.sort))
                .add($(self.selectors.filter))
                .off('.mixItUp');

            for (var i = 0; i < self._$targets.length; i++) {
                var target = self._$targets[i];

                hideAll && (target.style.display = '');

                delete target.mixParent;
            }

            self._execAction('destroy', 1, arguments);

            if (filters[self.selectors.filter] && filters[self.selectors.filter] > 1) {
                filters[self.selectors.filter]--;
            } else if (filters[self.selectors.filter] === 1) {
                delete filters[self.selectors.filter];
            }

            if (sorts[self.selectors.sort] && sorts[self.selectors.sort] > 1) {
                sorts[self.selectors.sort]--;
            } else if (sorts[self.selectors.sort] === 1) {
                delete sorts[self.selectors.sort];
            }

            delete $.MixItUp.prototype._instances[self._id];
        }

    };

    /* jQuery Methods
    ---------------------------------------------------------------------- */

    /**
     * jQuery .mixItUp() method
     * @since 2.0.0
     * @extends $.fn
     */

    $.fn.mixItUp = function () {
        var args = arguments,
            dataReturn = [],
            eachReturn,
            _instantiate = function (domNode, settings) {
                var instance = new $.MixItUp(),
                    rand = function () {
                        return ('00000' + (Math.random() * 16777216 << 0).toString(16)).substr(-6).toUpperCase();
                    };

                instance._execAction('_instantiate', 0, arguments);

                domNode.id = !domNode.id ? 'MixItUp' + rand() : domNode.id;

                if (!instance._instances[domNode.id]) {
                    instance._instances[domNode.id] = instance;
                    instance._init(domNode, settings);
                }

                instance._execAction('_instantiate', 1, arguments);
            };

        eachReturn = this.each(function () {
            if (args && typeof args[0] === 'string') {
                var instance = $.MixItUp.prototype._instances[this.id];
                if (args[0] === 'isLoaded') {
                    dataReturn.push(instance ? true : false);
                } else {
                    var data = instance[args[0]](args[1], args[2], args[3]);
                    if (data !== undf) dataReturn.push(data);
                }
            } else {
                _instantiate(this, args[0]);
            }
        });

        if (dataReturn.length) {
            return dataReturn.length > 1 ? dataReturn : dataReturn[0];
        } else {
            return eachReturn;
        }
    };

    /**
     * jQuery .removeStyle() method
     * @since 2.0.0
     * @extends $.fn
     */

    $.fn.removeStyle = function (style, prefix) {
        prefix = prefix ? prefix : '';

        return this.each(function () {
            var el = this,
                styles = style.split(' ');

            for (var i = 0; i < styles.length; i++) {
                for (var j = 0; j < 4; j++) {
                    switch (j) {
                        case 0:
                            var prop = styles[i];
                            break;
                        case 1:
                            var prop = $.MixItUp.prototype._helpers._camelCase(prop);
                            break;
                        case 2:
                            var prop = prefix + styles[i];
                            break;
                        case 3:
                            var prop = $.MixItUp.prototype._helpers._camelCase(prefix + styles[i]);
                    }

                    if (
                        el.style[prop] !== undf &&
                        typeof el.style[prop] !== 'unknown' &&
                        el.style[prop].length > 0
                    ) {
                        el.style[prop] = '';
                    }

                    if (!prefix && j === 1) break;
                }
            }

            if (el.attributes && el.attributes.style && el.attributes.style !== undf && el.attributes.style.value === '') {
                el.attributes.removeNamedItem('style');
            }
        });
    };

})(jQuery);
//fancybox

/*! fancyBox v2.1.5 fancyapps.com | fancyapps.com/fancybox/#license */
(function (s, H, f, w) {
    var K = f("html"), q = f(s), p = f(H), b = f.fancybox = function () { b.open.apply(this, arguments) }, J = navigator.userAgent.match(/msie/i), C = null, t = H.createTouch !== w, u = function (a) { return a && a.hasOwnProperty && a instanceof f }, r = function (a) { return a && "string" === f.type(a) }, F = function (a) { return r(a) && 0 < a.indexOf("%") }, m = function (a, d) { var e = parseInt(a, 10) || 0; d && F(a) && (e *= b.getViewport()[d] / 100); return Math.ceil(e) }, x = function (a, b) { return m(a, b) + "px" }; f.extend(b, {
        version: "2.1.5", defaults: {
            padding: 15, margin: 20,
            width: 800, height: 600, minWidth: 100, minHeight: 100, maxWidth: 9999, maxHeight: 9999, pixelRatio: 1, autoSize: !0, autoHeight: !1, autoWidth: !1, autoResize: !0, autoCenter: !t, fitToView: !0, aspectRatio: !1, topRatio: 0.5, leftRatio: 0.5, scrolling: "auto", wrapCSS: "", arrows: !0, closeBtn: !0, closeClick: !1, nextClick: !1, mouseWheel: !0, autoPlay: !1, playSpeed: 3E3, preload: 3, modal: !1, loop: !0, ajax: { dataType: "html", headers: { "X-fancyBox": !0 } }, iframe: { scrolling: "auto", preload: !0 }, swf: { wmode: "transparent", allowfullscreen: "true", allowscriptaccess: "always" },
            keys: { next: { 13: "left", 34: "up", 39: "left", 40: "up" }, prev: { 8: "right", 33: "down", 37: "right", 38: "down" }, close: [27], play: [32], toggle: [70] }, direction: { next: "left", prev: "right" }, scrollOutside: !0, index: 0, type: null, href: null, content: null, title: null, tpl: {
                wrap: '<div class="fancybox-wrap" tabIndex="-1"><div class="fancybox-skin"><div class="fancybox-outer"><div class="fancybox-inner"></div></div></div></div>', image: '<img class="fancybox-image" src="{href}" alt="" />', iframe: '<iframe id="fancybox-frame{rnd}" name="fancybox-frame{rnd}" class="fancybox-iframe" frameborder="0" vspace="0" hspace="0" webkitAllowFullScreen mozallowfullscreen allowFullScreen' +
                    (J ? ' allowtransparency="true"' : "") + "></iframe>", error: '<p class="fancybox-error">The requested content cannot be loaded.<br/>Please try again later.</p>', closeBtn: '<a title="Close" class="fancybox-item fancybox-close" href="javascript:;"></a>', next: '<a title="Next" class="fancybox-nav fancybox-next" href="javascript:;"><span></span></a>', prev: '<a title="Previous" class="fancybox-nav fancybox-prev" href="javascript:;"><span></span></a>'
            }, openEffect: "fade", openSpeed: 250, openEasing: "swing", openOpacity: !0,
            openMethod: "zoomIn", closeEffect: "fade", closeSpeed: 250, closeEasing: "swing", closeOpacity: !0, closeMethod: "zoomOut", nextEffect: "elastic", nextSpeed: 250, nextEasing: "swing", nextMethod: "changeIn", prevEffect: "elastic", prevSpeed: 250, prevEasing: "swing", prevMethod: "changeOut", helpers: { overlay: !0, title: !0 }, onCancel: f.noop, beforeLoad: f.noop, afterLoad: f.noop, beforeShow: f.noop, afterShow: f.noop, beforeChange: f.noop, beforeClose: f.noop, afterClose: f.noop
        }, group: {}, opts: {}, previous: null, coming: null, current: null, isActive: !1,
        isOpen: !1, isOpened: !1, wrap: null, skin: null, outer: null, inner: null, player: { timer: null, isActive: !1 }, ajaxLoad: null, imgPreload: null, transitions: {}, helpers: {}, open: function (a, d) {
            if (a && (f.isPlainObject(d) || (d = {}), !1 !== b.close(!0))) return f.isArray(a) || (a = u(a) ? f(a).get() : [a]), f.each(a, function (e, c) {
                var l = {}, g, h, k, n, m; "object" === f.type(c) && (c.nodeType && (c = f(c)), u(c) ? (l = { href: c.data("fancybox-href") || c.attr("href"), title: f("<div/>").text(c.data("fancybox-title") || c.attr("title")).html(), isDom: !0, element: c },
                    f.metadata && f.extend(!0, l, c.metadata())) : l = c); g = d.href || l.href || (r(c) ? c : null); h = d.title !== w ? d.title : l.title || ""; n = (k = d.content || l.content) ? "html" : d.type || l.type; !n && l.isDom && (n = c.data("fancybox-type"), n || (n = (n = c.prop("class").match(/fancybox\.(\w+)/)) ? n[1] : null)); r(g) && (n || (b.isImage(g) ? n = "image" : b.isSWF(g) ? n = "swf" : "#" === g.charAt(0) ? n = "inline" : r(c) && (n = "html", k = c)), "ajax" === n && (m = g.split(/\s+/, 2), g = m.shift(), m = m.shift())); k || ("inline" === n ? g ? k = f(r(g) ? g.replace(/.*(?=#[^\s]+$)/, "") : g) : l.isDom && (k = c) :
                        "html" === n ? k = g : n || g || !l.isDom || (n = "inline", k = c)); f.extend(l, { href: g, type: n, content: k, title: h, selector: m }); a[e] = l
            }), b.opts = f.extend(!0, {}, b.defaults, d), d.keys !== w && (b.opts.keys = d.keys ? f.extend({}, b.defaults.keys, d.keys) : !1), b.group = a, b._start(b.opts.index)
        }, cancel: function () {
            var a = b.coming; a && !1 === b.trigger("onCancel") || (b.hideLoading(), a && (b.ajaxLoad && b.ajaxLoad.abort(), b.ajaxLoad = null, b.imgPreload && (b.imgPreload.onload = b.imgPreload.onerror = null), a.wrap && a.wrap.stop(!0, !0).trigger("onReset").remove(),
                b.coming = null, b.current || b._afterZoomOut(a)))
        }, close: function (a) { b.cancel(); !1 !== b.trigger("beforeClose") && (b.unbindEvents(), b.isActive && (b.isOpen && !0 !== a ? (b.isOpen = b.isOpened = !1, b.isClosing = !0, f(".fancybox-item, .fancybox-nav").remove(), b.wrap.stop(!0, !0).removeClass("fancybox-opened"), b.transitions[b.current.closeMethod]()) : (f(".fancybox-wrap").stop(!0).trigger("onReset").remove(), b._afterZoomOut()))) }, play: function (a) {
            var d = function () { clearTimeout(b.player.timer) }, e = function () {
                d(); b.current && b.player.isActive &&
                    (b.player.timer = setTimeout(b.next, b.current.playSpeed))
            }, c = function () { d(); p.unbind(".player"); b.player.isActive = !1; b.trigger("onPlayEnd") }; !0 === a || !b.player.isActive && !1 !== a ? b.current && (b.current.loop || b.current.index < b.group.length - 1) && (b.player.isActive = !0, p.bind({ "onCancel.player beforeClose.player": c, "onUpdate.player": e, "beforeLoad.player": d }), e(), b.trigger("onPlayStart")) : c()
        }, next: function (a) { var d = b.current; d && (r(a) || (a = d.direction.next), b.jumpto(d.index + 1, a, "next")) }, prev: function (a) {
            var d =
                b.current; d && (r(a) || (a = d.direction.prev), b.jumpto(d.index - 1, a, "prev"))
        }, jumpto: function (a, d, e) { var c = b.current; c && (a = m(a), b.direction = d || c.direction[a >= c.index ? "next" : "prev"], b.router = e || "jumpto", c.loop && (0 > a && (a = c.group.length + a % c.group.length), a %= c.group.length), c.group[a] !== w && (b.cancel(), b._start(a))) }, reposition: function (a, d) { var e = b.current, c = e ? e.wrap : null, l; c && (l = b._getPosition(d), a && "scroll" === a.type ? (delete l.position, c.stop(!0, !0).animate(l, 200)) : (c.css(l), e.pos = f.extend({}, e.dim, l))) },
        update: function (a) { var d = a && a.originalEvent && a.originalEvent.type, e = !d || "orientationchange" === d; e && (clearTimeout(C), C = null); b.isOpen && !C && (C = setTimeout(function () { var c = b.current; c && !b.isClosing && (b.wrap.removeClass("fancybox-tmp"), (e || "load" === d || "resize" === d && c.autoResize) && b._setDimension(), "scroll" === d && c.canShrink || b.reposition(a), b.trigger("onUpdate"), C = null) }, e && !t ? 0 : 300)) }, toggle: function (a) {
        b.isOpen && (b.current.fitToView = "boolean" === f.type(a) ? a : !b.current.fitToView, t && (b.wrap.removeAttr("style").addClass("fancybox-tmp"),
            b.trigger("onUpdate")), b.update())
        }, hideLoading: function () { p.unbind(".loading"); f("#fancybox-loading").remove() }, showLoading: function () { var a, d; b.hideLoading(); a = f('<div id="fancybox-loading"><div></div></div>').click(b.cancel).appendTo("body"); p.bind("keydown.loading", function (a) { 27 === (a.which || a.keyCode) && (a.preventDefault(), b.cancel()) }); b.defaults.fixed || (d = b.getViewport(), a.css({ position: "absolute", top: 0.5 * d.h + d.y, left: 0.5 * d.w + d.x })); b.trigger("onLoading") }, getViewport: function () {
            var a = b.current &&
                b.current.locked || !1, d = { x: q.scrollLeft(), y: q.scrollTop() }; a && a.length ? (d.w = a[0].clientWidth, d.h = a[0].clientHeight) : (d.w = t && s.innerWidth ? s.innerWidth : q.width(), d.h = t && s.innerHeight ? s.innerHeight : q.height()); return d
        }, unbindEvents: function () { b.wrap && u(b.wrap) && b.wrap.unbind(".fb"); p.unbind(".fb"); q.unbind(".fb") }, bindEvents: function () {
            var a = b.current, d; a && (q.bind("orientationchange.fb" + (t ? "" : " resize.fb") + (a.autoCenter && !a.locked ? " scroll.fb" : ""), b.update), (d = a.keys) && p.bind("keydown.fb", function (e) {
                var c =
                    e.which || e.keyCode, l = e.target || e.srcElement; if (27 === c && b.coming) return !1; e.ctrlKey || e.altKey || e.shiftKey || e.metaKey || l && (l.type || f(l).is("[contenteditable]")) || f.each(d, function (d, l) { if (1 < a.group.length && l[c] !== w) return b[d](l[c]), e.preventDefault(), !1; if (-1 < f.inArray(c, l)) return b[d](), e.preventDefault(), !1 })
            }), f.fn.mousewheel && a.mouseWheel && b.wrap.bind("mousewheel.fb", function (d, c, l, g) {
                for (var h = f(d.target || null), k = !1; h.length && !(k || h.is(".fancybox-skin") || h.is(".fancybox-wrap"));)k = h[0] && !(h[0].style.overflow &&
                    "hidden" === h[0].style.overflow) && (h[0].clientWidth && h[0].scrollWidth > h[0].clientWidth || h[0].clientHeight && h[0].scrollHeight > h[0].clientHeight), h = f(h).parent(); 0 !== c && !k && 1 < b.group.length && !a.canShrink && (0 < g || 0 < l ? b.prev(0 < g ? "down" : "left") : (0 > g || 0 > l) && b.next(0 > g ? "up" : "right"), d.preventDefault())
            }))
        }, trigger: function (a, d) {
            var e, c = d || b.coming || b.current; if (c) {
                f.isFunction(c[a]) && (e = c[a].apply(c, Array.prototype.slice.call(arguments, 1))); if (!1 === e) return !1; c.helpers && f.each(c.helpers, function (d, e) {
                    if (e &&
                        b.helpers[d] && f.isFunction(b.helpers[d][a])) b.helpers[d][a](f.extend(!0, {}, b.helpers[d].defaults, e), c)
                })
            } p.trigger(a)
        }, isImage: function (a) { return r(a) && a.match(/(^data:image\/.*,)|(\.(jp(e|g|eg)|gif|png|bmp|webp|svg)((\?|#).*)?$)/i) }, isSWF: function (a) { return r(a) && a.match(/\.(swf)((\?|#).*)?$/i) }, _start: function (a) {
            var d = {}, e, c; a = m(a); e = b.group[a] || null; if (!e) return !1; d = f.extend(!0, {}, b.opts, e); e = d.margin; c = d.padding; "number" === f.type(e) && (d.margin = [e, e, e, e]); "number" === f.type(c) && (d.padding = [c, c,
                c, c]); d.modal && f.extend(!0, d, { closeBtn: !1, closeClick: !1, nextClick: !1, arrows: !1, mouseWheel: !1, keys: null, helpers: { overlay: { closeClick: !1 } } }); d.autoSize && (d.autoWidth = d.autoHeight = !0); "auto" === d.width && (d.autoWidth = !0); "auto" === d.height && (d.autoHeight = !0); d.group = b.group; d.index = a; b.coming = d; if (!1 === b.trigger("beforeLoad")) b.coming = null; else {
                    c = d.type; e = d.href; if (!c) return b.coming = null, b.current && b.router && "jumpto" !== b.router ? (b.current.index = a, b[b.router](b.direction)) : !1; b.isActive = !0; if ("image" ===
                        c || "swf" === c) d.autoHeight = d.autoWidth = !1, d.scrolling = "visible"; "image" === c && (d.aspectRatio = !0); "iframe" === c && t && (d.scrolling = "scroll"); d.wrap = f(d.tpl.wrap).addClass("fancybox-" + (t ? "mobile" : "desktop") + " fancybox-type-" + c + " fancybox-tmp " + d.wrapCSS).appendTo(d.parent || "body"); f.extend(d, { skin: f(".fancybox-skin", d.wrap), outer: f(".fancybox-outer", d.wrap), inner: f(".fancybox-inner", d.wrap) }); f.each(["Top", "Right", "Bottom", "Left"], function (a, b) { d.skin.css("padding" + b, x(d.padding[a])) }); b.trigger("onReady");
                    if ("inline" === c || "html" === c) { if (!d.content || !d.content.length) return b._error("content") } else if (!e) return b._error("href"); "image" === c ? b._loadImage() : "ajax" === c ? b._loadAjax() : "iframe" === c ? b._loadIframe() : b._afterLoad()
                }
        }, _error: function (a) { f.extend(b.coming, { type: "html", autoWidth: !0, autoHeight: !0, minWidth: 0, minHeight: 0, scrolling: "no", hasError: a, content: b.coming.tpl.error }); b._afterLoad() }, _loadImage: function () {
            var a = b.imgPreload = new Image; a.onload = function () {
            this.onload = this.onerror = null; b.coming.width =
                this.width / b.opts.pixelRatio; b.coming.height = this.height / b.opts.pixelRatio; b._afterLoad()
            }; a.onerror = function () { this.onload = this.onerror = null; b._error("image") }; a.src = b.coming.href; !0 !== a.complete && b.showLoading()
        }, _loadAjax: function () { var a = b.coming; b.showLoading(); b.ajaxLoad = f.ajax(f.extend({}, a.ajax, { url: a.href, error: function (a, e) { b.coming && "abort" !== e ? b._error("ajax", a) : b.hideLoading() }, success: function (d, e) { "success" === e && (a.content = d, b._afterLoad()) } })) }, _loadIframe: function () {
            var a = b.coming,
            d = f(a.tpl.iframe.replace(/\{rnd\}/g, (new Date).getTime())).attr("scrolling", t ? "auto" : a.iframe.scrolling).attr("src", a.href); f(a.wrap).bind("onReset", function () { try { f(this).find("iframe").hide().attr("src", "//about:blank").end().empty() } catch (a) { } }); a.iframe.preload && (b.showLoading(), d.one("load", function () { f(this).data("ready", 1); t || f(this).bind("load.fb", b.update); f(this).parents(".fancybox-wrap").width("100%").removeClass("fancybox-tmp").show(); b._afterLoad() })); a.content = d.appendTo(a.inner); a.iframe.preload ||
                b._afterLoad()
        }, _preloadImages: function () { var a = b.group, d = b.current, e = a.length, c = d.preload ? Math.min(d.preload, e - 1) : 0, f, g; for (g = 1; g <= c; g += 1)f = a[(d.index + g) % e], "image" === f.type && f.href && ((new Image).src = f.href) }, _afterLoad: function () {
            var a = b.coming, d = b.current, e, c, l, g, h; b.hideLoading(); if (a && !1 !== b.isActive) if (!1 === b.trigger("afterLoad", a, d)) a.wrap.stop(!0).trigger("onReset").remove(), b.coming = null; else {
                d && (b.trigger("beforeChange", d), d.wrap.stop(!0).removeClass("fancybox-opened").find(".fancybox-item, .fancybox-nav").remove());
                b.unbindEvents(); e = a.content; c = a.type; l = a.scrolling; f.extend(b, { wrap: a.wrap, skin: a.skin, outer: a.outer, inner: a.inner, current: a, previous: d }); g = a.href; switch (c) {
                    case "inline": case "ajax": case "html": a.selector ? e = f("<div>").html(e).find(a.selector) : u(e) && (e.data("fancybox-placeholder") || e.data("fancybox-placeholder", f('<div class="fancybox-placeholder"></div>').insertAfter(e).hide()), e = e.show().detach(), a.wrap.bind("onReset", function () {
                        f(this).find(e).length && e.hide().replaceAll(e.data("fancybox-placeholder")).data("fancybox-placeholder",
                            !1)
                    })); break; case "image": e = a.tpl.image.replace(/\{href\}/g, g); break; case "swf": e = '<object id="fancybox-swf" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="100%" height="100%"><param name="movie" value="' + g + '"></param>', h = "", f.each(a.swf, function (a, b) { e += '<param name="' + a + '" value="' + b + '"></param>'; h += " " + a + '="' + b + '"' }), e += '<embed src="' + g + '" type="application/x-shockwave-flash" width="100%" height="100%"' + h + "></embed></object>"
                }u(e) && e.parent().is(a.inner) || a.inner.append(e); b.trigger("beforeShow");
                a.inner.css("overflow", "yes" === l ? "scroll" : "no" === l ? "hidden" : l); b._setDimension(); b.reposition(); b.isOpen = !1; b.coming = null; b.bindEvents(); if (!b.isOpened) f(".fancybox-wrap").not(a.wrap).stop(!0).trigger("onReset").remove(); else if (d.prevMethod) b.transitions[d.prevMethod](); b.transitions[b.isOpened ? a.nextMethod : a.openMethod](); b._preloadImages()
            }
        }, _setDimension: function () {
            var a = b.getViewport(), d = 0, e = !1, c = !1, e = b.wrap, l = b.skin, g = b.inner, h = b.current, c = h.width, k = h.height, n = h.minWidth, v = h.minHeight, p = h.maxWidth,
            q = h.maxHeight, t = h.scrolling, r = h.scrollOutside ? h.scrollbarWidth : 0, y = h.margin, z = m(y[1] + y[3]), s = m(y[0] + y[2]), w, A, u, D, B, G, C, E, I; e.add(l).add(g).width("auto").height("auto").removeClass("fancybox-tmp"); y = m(l.outerWidth(!0) - l.width()); w = m(l.outerHeight(!0) - l.height()); A = z + y; u = s + w; D = F(c) ? (a.w - A) * m(c) / 100 : c; B = F(k) ? (a.h - u) * m(k) / 100 : k; if ("iframe" === h.type) {
                if (I = h.content, h.autoHeight && 1 === I.data("ready")) try {
                    I[0].contentWindow.document.location && (g.width(D).height(9999), G = I.contents().find("body"), r && G.css("overflow-x",
                        "hidden"), B = G.outerHeight(!0))
                } catch (H) { }
            } else if (h.autoWidth || h.autoHeight) g.addClass("fancybox-tmp"), h.autoWidth || g.width(D), h.autoHeight || g.height(B), h.autoWidth && (D = g.width()), h.autoHeight && (B = g.height()), g.removeClass("fancybox-tmp"); c = m(D); k = m(B); E = D / B; n = m(F(n) ? m(n, "w") - A : n); p = m(F(p) ? m(p, "w") - A : p); v = m(F(v) ? m(v, "h") - u : v); q = m(F(q) ? m(q, "h") - u : q); G = p; C = q; h.fitToView && (p = Math.min(a.w - A, p), q = Math.min(a.h - u, q)); A = a.w - z; s = a.h - s; h.aspectRatio ? (c > p && (c = p, k = m(c / E)), k > q && (k = q, c = m(k * E)), c < n && (c = n, k = m(c /
                E)), k < v && (k = v, c = m(k * E))) : (c = Math.max(n, Math.min(c, p)), h.autoHeight && "iframe" !== h.type && (g.width(c), k = g.height()), k = Math.max(v, Math.min(k, q))); if (h.fitToView) if (g.width(c).height(k), e.width(c + y), a = e.width(), z = e.height(), h.aspectRatio) for (; (a > A || z > s) && c > n && k > v && !(19 < d++);)k = Math.max(v, Math.min(q, k - 10)), c = m(k * E), c < n && (c = n, k = m(c / E)), c > p && (c = p, k = m(c / E)), g.width(c).height(k), e.width(c + y), a = e.width(), z = e.height(); else c = Math.max(n, Math.min(c, c - (a - A))), k = Math.max(v, Math.min(k, k - (z - s))); r && "auto" === t && k < B &&
                    c + y + r < A && (c += r); g.width(c).height(k); e.width(c + y); a = e.width(); z = e.height(); e = (a > A || z > s) && c > n && k > v; c = h.aspectRatio ? c < G && k < C && c < D && k < B : (c < G || k < C) && (c < D || k < B); f.extend(h, { dim: { width: x(a), height: x(z) }, origWidth: D, origHeight: B, canShrink: e, canExpand: c, wPadding: y, hPadding: w, wrapSpace: z - l.outerHeight(!0), skinSpace: l.height() - k }); !I && h.autoHeight && k > v && k < q && !c && g.height("auto")
        }, _getPosition: function (a) {
            var d = b.current, e = b.getViewport(), c = d.margin, f = b.wrap.width() + c[1] + c[3], g = b.wrap.height() + c[0] + c[2], c = {
                position: "absolute",
                top: c[0], left: c[3]
            }; d.autoCenter && d.fixed && !a && g <= e.h && f <= e.w ? c.position = "fixed" : d.locked || (c.top += e.y, c.left += e.x); c.top = x(Math.max(c.top, c.top + (e.h - g) * d.topRatio)); c.left = x(Math.max(c.left, c.left + (e.w - f) * d.leftRatio)); return c
        }, _afterZoomIn: function () {
            var a = b.current; a && ((b.isOpen = b.isOpened = !0, b.wrap.css("overflow", "visible").addClass("fancybox-opened"), b.update(), (a.closeClick || a.nextClick && 1 < b.group.length) && b.inner.css("cursor", "pointer").bind("click.fb", function (d) {
                f(d.target).is("a") || f(d.target).parent().is("a") ||
                (d.preventDefault(), b[a.closeClick ? "close" : "next"]())
            }), a.closeBtn && f(a.tpl.closeBtn).appendTo(b.skin).bind("click.fb", function (a) { a.preventDefault(); b.close() }), a.arrows && 1 < b.group.length && ((a.loop || 0 < a.index) && f(a.tpl.prev).appendTo(b.outer).bind("click.fb", b.prev), (a.loop || a.index < b.group.length - 1) && f(a.tpl.next).appendTo(b.outer).bind("click.fb", b.next)), b.trigger("afterShow"), a.loop || a.index !== a.group.length - 1) ? b.opts.autoPlay && !b.player.isActive && (b.opts.autoPlay = !1, b.play(!0)) : b.play(!1))
        },
        _afterZoomOut: function (a) { a = a || b.current; f(".fancybox-wrap").trigger("onReset").remove(); f.extend(b, { group: {}, opts: {}, router: !1, current: null, isActive: !1, isOpened: !1, isOpen: !1, isClosing: !1, wrap: null, skin: null, outer: null, inner: null }); b.trigger("afterClose", a) }
    }); b.transitions = {
        getOrigPosition: function () {
            var a = b.current, d = a.element, e = a.orig, c = {}, f = 50, g = 50, h = a.hPadding, k = a.wPadding, n = b.getViewport(); !e && a.isDom && d.is(":visible") && (e = d.find("img:first"), e.length || (e = d)); u(e) ? (c = e.offset(), e.is("img") &&
                (f = e.outerWidth(), g = e.outerHeight())) : (c.top = n.y + (n.h - g) * a.topRatio, c.left = n.x + (n.w - f) * a.leftRatio); if ("fixed" === b.wrap.css("position") || a.locked) c.top -= n.y, c.left -= n.x; return c = { top: x(c.top - h * a.topRatio), left: x(c.left - k * a.leftRatio), width: x(f + k), height: x(g + h) }
        }, step: function (a, d) {
            var e, c, f = d.prop; c = b.current; var g = c.wrapSpace, h = c.skinSpace; if ("width" === f || "height" === f) e = d.end === d.start ? 1 : (a - d.start) / (d.end - d.start), b.isClosing && (e = 1 - e), c = "width" === f ? c.wPadding : c.hPadding, c = a - c, b.skin[f](m("width" ===
                f ? c : c - g * e)), b.inner[f](m("width" === f ? c : c - g * e - h * e))
        }, zoomIn: function () { var a = b.current, d = a.pos, e = a.openEffect, c = "elastic" === e, l = f.extend({ opacity: 1 }, d); delete l.position; c ? (d = this.getOrigPosition(), a.openOpacity && (d.opacity = 0.1)) : "fade" === e && (d.opacity = 0.1); b.wrap.css(d).animate(l, { duration: "none" === e ? 0 : a.openSpeed, easing: a.openEasing, step: c ? this.step : null, complete: b._afterZoomIn }) }, zoomOut: function () {
            var a = b.current, d = a.closeEffect, e = "elastic" === d, c = { opacity: 0.1 }; e && (c = this.getOrigPosition(), a.closeOpacity &&
                (c.opacity = 0.1)); b.wrap.animate(c, { duration: "none" === d ? 0 : a.closeSpeed, easing: a.closeEasing, step: e ? this.step : null, complete: b._afterZoomOut })
        }, changeIn: function () { var a = b.current, d = a.nextEffect, e = a.pos, c = { opacity: 1 }, f = b.direction, g; e.opacity = 0.1; "elastic" === d && (g = "down" === f || "up" === f ? "top" : "left", "down" === f || "right" === f ? (e[g] = x(m(e[g]) - 200), c[g] = "+=200px") : (e[g] = x(m(e[g]) + 200), c[g] = "-=200px")); "none" === d ? b._afterZoomIn() : b.wrap.css(e).animate(c, { duration: a.nextSpeed, easing: a.nextEasing, complete: b._afterZoomIn }) },
        changeOut: function () { var a = b.previous, d = a.prevEffect, e = { opacity: 0.1 }, c = b.direction; "elastic" === d && (e["down" === c || "up" === c ? "top" : "left"] = ("up" === c || "left" === c ? "-" : "+") + "=200px"); a.wrap.animate(e, { duration: "none" === d ? 0 : a.prevSpeed, easing: a.prevEasing, complete: function () { f(this).trigger("onReset").remove() } }) }
    }; b.helpers.overlay = {
        defaults: { closeClick: !0, speedOut: 200, showEarly: !0, css: {}, locked: !t, fixed: !0 }, overlay: null, fixed: !1, el: f("html"), create: function (a) {
            var d; a = f.extend({}, this.defaults, a); this.overlay &&
                this.close(); d = b.coming ? b.coming.parent : a.parent; this.overlay = f('<div class="fancybox-overlay"></div>').appendTo(d && d.lenth ? d : "body"); this.fixed = !1; a.fixed && b.defaults.fixed && (this.overlay.addClass("fancybox-overlay-fixed"), this.fixed = !0)
        }, open: function (a) {
            var d = this; a = f.extend({}, this.defaults, a); this.overlay ? this.overlay.unbind(".overlay").width("auto").height("auto") : this.create(a); this.fixed || (q.bind("resize.overlay", f.proxy(this.update, this)), this.update()); a.closeClick && this.overlay.bind("click.overlay",
                function (a) { if (f(a.target).hasClass("fancybox-overlay")) return b.isActive ? b.close() : d.close(), !1 }); this.overlay.css(a.css).show()
        }, close: function () { q.unbind("resize.overlay"); this.el.hasClass("fancybox-lock") && (f(".fancybox-margin").removeClass("fancybox-margin"), this.el.removeClass("fancybox-lock"), q.scrollTop(this.scrollV).scrollLeft(this.scrollH)); f(".fancybox-overlay").remove().hide(); f.extend(this, { overlay: null, fixed: !1 }) }, update: function () {
            var a = "100%", b; this.overlay.width(a).height("100%");
            J ? (b = Math.max(H.documentElement.offsetWidth, H.body.offsetWidth), p.width() > b && (a = p.width())) : p.width() > q.width() && (a = p.width()); this.overlay.width(a).height(p.height())
        }, onReady: function (a, b) { var e = this.overlay; f(".fancybox-overlay").stop(!0, !0); e || this.create(a); a.locked && this.fixed && b.fixed && (b.locked = this.overlay.append(b.wrap), b.fixed = !1); !0 === a.showEarly && this.beforeShow.apply(this, arguments) }, beforeShow: function (a, b) {
        b.locked && !this.el.hasClass("fancybox-lock") && (!1 !== this.fixPosition && f("*").filter(function () {
            return "fixed" ===
                f(this).css("position") && !f(this).hasClass("fancybox-overlay") && !f(this).hasClass("fancybox-wrap")
        }).addClass("fancybox-margin"), this.el.addClass("fancybox-margin"), this.scrollV = q.scrollTop(), this.scrollH = q.scrollLeft(), this.el.addClass("fancybox-lock"), q.scrollTop(this.scrollV).scrollLeft(this.scrollH)); this.open(a)
        }, onUpdate: function () { this.fixed || this.update() }, afterClose: function (a) { this.overlay && !b.coming && this.overlay.fadeOut(a.speedOut, f.proxy(this.close, this)) }
    }; b.helpers.title = {
        defaults: {
            type: "float",
            position: "bottom"
        }, beforeShow: function (a) {
            var d = b.current, e = d.title, c = a.type; f.isFunction(e) && (e = e.call(d.element, d)); if (r(e) && "" !== f.trim(e)) {
                d = f('<div class="fancybox-title fancybox-title-' + c + '-wrap">' + e + "</div>"); switch (c) { case "inside": c = b.skin; break; case "outside": c = b.wrap; break; case "over": c = b.inner; break; default: c = b.skin, d.appendTo("body"), J && d.width(d.width()), d.wrapInner('<span class="child"></span>'), b.current.margin[2] += Math.abs(m(d.css("margin-bottom"))) }d["top" === a.position ? "prependTo" :
                    "appendTo"](c)
            }
        }
    }; f.fn.fancybox = function (a) {
        var d, e = f(this), c = this.selector || "", l = function (g) { var h = f(this).blur(), k = d, l, m; g.ctrlKey || g.altKey || g.shiftKey || g.metaKey || h.is(".fancybox-wrap") || (l = a.groupAttr || "data-fancybox-group", m = h.attr(l), m || (l = "rel", m = h.get(0)[l]), m && "" !== m && "nofollow" !== m && (h = c.length ? f(c) : e, h = h.filter("[" + l + '="' + m + '"]'), k = h.index(this)), a.index = k, !1 !== b.open(h, a) && g.preventDefault()) }; a = a || {}; d = a.index || 0; c && !1 !== a.live ? p.undelegate(c, "click.fb-start").delegate(c + ":not('.fancybox-item, .fancybox-nav')",
            "click.fb-start", l) : e.unbind("click.fb-start").bind("click.fb-start", l); this.filter("[data-fancybox-start=1]").trigger("click"); return this
    }; p.ready(function () {
        var a, d; f.scrollbarWidth === w && (f.scrollbarWidth = function () { var a = f('<div style="width:50px;height:50px;overflow:auto"><div/></div>').appendTo("body"), b = a.children(), b = b.innerWidth() - b.height(99).innerWidth(); a.remove(); return b }); f.support.fixedPosition === w && (f.support.fixedPosition = function () {
            var a = f('<div style="position:fixed;top:20px;"></div>').appendTo("body"),
            b = 20 === a[0].offsetTop || 15 === a[0].offsetTop; a.remove(); return b
        }()); f.extend(b.defaults, { scrollbarWidth: f.scrollbarWidth(), fixed: f.support.fixedPosition, parent: f("body") }); a = f(s).width(); K.addClass("fancybox-lock-test"); d = f(s).width(); K.removeClass("fancybox-lock-test"); f("<style type='text/css'>.fancybox-margin{margin-right:" + (d - a) + "px;}</style>").appendTo("head")
    })
})(window, document, jQuery);




//Newsbox

/*
* jQuery Bootstrap News Box v1.0.1
* 
* Copyright 2014, Dragan Mitrovic
* email: gagi270683@gmail.com
* Free to use and abuse under the MIT license.
* http://www.opensource.org/licenses/mit-license.php
*/
if (typeof Object.create !== "function") { Object.create = function (e) { function t() { } t.prototype = e; return new t } } (function (e, t, n, r) { var i = { init: function (t, n) { var r = this; r.elem = n; r.$elem = e(n); r.newsTagName = r.$elem.find(":first-child").prop("tagName"); r.newsClassName = r.$elem.find(":first-child").attr("class"); r.timer = null; r.resizeTimer = null; r.animationStarted = false; r.isHovered = false; if (typeof t === "string") { if (console) { console.error("String property override is not supported") } throw "String property override is not supported" } else { r.options = e.extend({}, e.fn.bootstrapNews.options, t); r.prepareLayout(); if (r.options.autoplay) { r.animate() } if (r.options.navigation) { r.buildNavigation() } if (typeof r.options.onToDo === "function") { r.options.onToDo.apply(r, arguments) } } }, prepareLayout: function () { var n = this; e(n.elem).find("." + n.newsClassName).on("mouseenter", function () { n.onReset(true) }); e(n.elem).find("." + n.newsClassName).on("mouseout", function () { n.onReset(false) }); e.map(n.$elem.find(n.newsTagName), function (t, r) { if (r > n.options.newsPerPage - 1) { e(t).hide() } else { e(t).show() } }); if (n.$elem.find(n.newsTagName).length < n.options.newsPerPage) { n.options.newsPerPage = n.$elem.find(n.newsTagName).length } var r = 0; e.map(n.$elem.find(n.newsTagName), function (t, i) { if (i < n.options.newsPerPage) { r = parseInt(r) + parseInt(e(t).height()) + 10 } }); e(n.elem).css({ "overflow-y": "hidden", height: r }); e(t).resize(function () { if (n.resizeTimer !== null) { clearTimeout(n.resizeTimer) } n.resizeTimer = setTimeout(function () { n.prepareLayout() }, 200) }) }, findPanelObject: function () { var e = this.$elem; while (e.parent() !== r) { e = e.parent(); if (e.parent().hasClass("panel")) { return e.parent() } } return r }, buildNavigation: function () { var t = this.findPanelObject(); if (t) { var n = '<ul class="pagination pull-right" style="margin: 0px;">' + '<li><a href="#" class="prev"><span class="glyphicon glyphicon-chevron-down"></span></a></li>' + '<li><a href="#" class="next"><span class="glyphicon glyphicon-chevron-up"></span></a></li>' + '</ul><div class="clearfix"></div>'; var r = e(t).find(".panel-footer")[0]; if (r) { e(r).append(n) } else { e(t).append('<div class="panel-footer">' + n + "</div>") } var i = this; e(t).find(".prev").on("click", function (e) { e.preventDefault(); i.onPrev() }); e(t).find(".next").on("click", function (e) { e.preventDefault(); i.onNext() }) } }, onStop: function () { }, onPause: function () { var e = this; e.isHovered = true; if (this.options.autoplay && e.timer) { clearTimeout(e.timer) } }, onReset: function (e) { var t = this; if (t.timer) { clearTimeout(t.timer) } if (t.options.autoplay) { t.isHovered = e; t.animate() } }, animate: function () { var e = this; e.timer = setTimeout(function () { if (!e.options.pauseOnHover) { e.isHovered = false } if (!e.isHovered) { if (e.options.direction === "up") { e.onNext() } else { e.onPrev() } } }, e.options.newsTickerInterval) }, onPrev: function () { var t = this; if (t.animationStarted) { return false } t.animationStarted = true; var n = "<" + t.newsTagName + ' style="display:none;" class="' + t.newsClassName + '">' + e(t.$elem).find(t.newsTagName).last().html() + "</" + t.newsTagName + ">"; e(t.$elem).prepend(n); e(t.$elem).find(t.newsTagName).first().slideDown(t.options.animationSpeed, function () { e(t.$elem).find(t.newsTagName).last().remove() }); e(t.$elem).find(t.newsTagName + ":nth-child(" + parseInt(t.options.newsPerPage + 1) + ")").slideUp(t.options.animationSpeed, function () { t.animationStarted = false; t.onReset(t.isHovered) }); e(t.elem).find("." + t.newsClassName).on("mouseenter", function () { t.onReset(true) }); e(t.elem).find("." + t.newsClassName).on("mouseout", function () { t.onReset(false) }) }, onNext: function () { var t = this; if (t.animationStarted) { return false } t.animationStarted = true; var n = "<" + t.newsTagName + ' style="display:none;" class=' + t.newsClassName + ">" + e(t.$elem).find(t.newsTagName).first().html() + "</" + t.newsTagName + ">"; e(t.$elem).append(n); e(t.$elem).find(t.newsTagName).first().slideUp(t.options.animationSpeed, function () { e(this).remove() }); e(t.$elem).find(t.newsTagName + ":nth-child(" + parseInt(t.options.newsPerPage + 1) + ")").slideDown(t.options.animationSpeed, function () { t.animationStarted = false; t.onReset(t.isHovered) }); e(t.elem).find("." + t.newsClassName).on("mouseenter", function () { t.onReset(true) }); e(t.elem).find("." + t.newsClassName).on("mouseout", function () { t.onReset(false) }) } }; e.fn.bootstrapNews = function (e) { return this.each(function () { var t = Object.create(i); t.init(e, this) }) }; e.fn.bootstrapNews.options = { newsPerPage: 4, navigation: true, autoplay: true, direction: "up", animationSpeed: "normal", newsTickerInterval: 4e3, pauseOnHover: true, onStop: null, onPause: null, onReset: null, onPrev: null, onNext: null, onToDo: null } })(jQuery, window, document)





//1. SEARCH FORM
//2. ABOUT US VIDEO
//2. TOP SLIDER
//3. ABOUT US(SLICK SLIDER)
//4. LATEST COURSE SLIDER(SLICK SLIDER)
//5. TESTIMONIAL SLIDER(SLICK SLIDER)
//6. COUNTER
//7. RELATED ITEM SLIDER(SLICK SLIDER)
//8. MIXIT FILTER(FOR GALLERY)
//9. FANCYBOX(FOR PORTFOLIO POPUP VIEW)
//11. HOVER DROPDOWN MENU
//12. SCROLL TOP BUTTON


 

jQuery(function ($) {


    /* ----------------------------------------------------------- */
    /*  1. SEARCH FORM
    /* ----------------------------------------------------------- */

    jQuery('#mu-search-icon').on('click', function (event) {
        event.preventDefault();
        $('#mu-search').addClass('mu-search-open');
        $('#mu-search form input[type="search"]').focus();
    });

    jQuery('.mu-search-close').on('click', function (event) {
        $("#mu-search").removeClass('mu-search-open');
    });

    /* ----------------------------------------------------------- */
    /*  2. ABOUT US VIDEO
    /* ----------------------------------------------------------- */
    // WHEN CLICK PLAY BUTTON 
    jQuery('#mu-abtus-video').on('click', function (event) {
        event.preventDefault();
        $('body').append("<div id='about-video-popup'><span id='mu-video-close' class='fa fa-close'></span><iframe id='mutube-video' name='mutube-video' frameborder='0' allowfullscreen></iframe></div>");
        $("#mutube-video").attr("src", $(this).attr("href"));
    });
    // WHEN CLICK CLOSE BUTTON
    $(document).on('click', '#mu-video-close', function (event) {
        $(this).parent("div").fadeOut(1000);
    });
    // WHEN CLICK OVERLAY BACKGROUND
    $(document).on('click', '#about-video-popup', function (event) {
        $(this).remove();
    });

    /* ----------------------------------------------------------- */
    /*  3. TOP SLIDER (SLICK SLIDER)
    /* ----------------------------------------------------------- */

    jQuery('#mu-slider').slick({
        dots: false,
        infinite: true,
        arrows: true,
        speed: 500,
        autoplay: true,
        cssEase: 'linear'
    });

    /* ----------------------------------------------------------- */
    /*  4. ABOUT US (SLICK SLIDER)
    /* ----------------------------------------------------------- */

    jQuery('#mu-testimonial-slide').slick({
        dots: true,
        infinite: true,
        arrows: false,
        speed: 500,
        autoplay: true,
        cssEase: 'linear'
    });


    /* ----------------------------------------------------------- */
    /*  5. LATEST COURSE SLIDER (SLICK SLIDER)
    /* ----------------------------------------------------------- */

    jQuery('#mu-latest-course-slide').slick({
        dots: true,
        arrows: false,
        infinite: true,
        speed: 300,
        slidesToShow: 3,
        slidesToScroll: 2,
        autoplay: true,
        autoplaySpeed: 2500,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                    infinite: true,
                    dots: true
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
            // You can unslick at a given breakpoint now by adding:
            // settings: "unslick"
            // instead of a settings object
        ]
    });

    /* ----------------------------------------------------------- */
    /*  6. TESTIMONIAL SLIDER (SLICK SLIDER)
    /* ----------------------------------------------------------- */

    jQuery('.mu-testimonial-slider').slick({
        dots: true,
        infinite: true,
        arrows: false,
        autoplay: true,
        speed: 500,
        cssEase: 'linear'
    });

    /* ----------------------------------------------------------- */
    /*  7. COUNTER
    /* ----------------------------------------------------------- */

    jQuery('.counter').counterUp({
        delay: 10,
        time: 1000
    });


    /* ----------------------------------------------------------- */
    /*  8. RELATED ITEM SLIDER (SLICK SLIDER)
    /* ----------------------------------------------------------- */

    jQuery('#mu-related-item-slide').slick({
        dots: false,
        arrows: true,
        infinite: true,
        speed: 300,
        slidesToShow: 2,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 2500,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                    infinite: true,
                    dots: false
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
            // You can unslick at a given breakpoint now by adding:
            // settings: "unslick"
            // instead of a settings object
        ]
    });

    /* ----------------------------------------------------------- */
    /*  9. MIXIT FILTER (FOR GALLERY) 
    /* ----------------------------------------------------------- */

    jQuery(function () {
        jQuery('#mixit-container').mixItUp();
    });

    /* ----------------------------------------------------------- */
    /*  10. FANCYBOX (FOR PORTFOLIO POPUP VIEW) 
    /* ----------------------------------------------------------- */

    jQuery(document).ready(function () {
        jQuery(".fancybox").fancybox();
    });

    /* ----------------------------------------------------------- */
    /*  11. HOVER DROPDOWN MENU
    /* ----------------------------------------------------------- */

    // for hover dropdown menu
    jQuery('ul.nav li.dropdown').hover(function () {
        jQuery(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(200);
    }, function () {
        jQuery(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(200);
    });


    /* ----------------------------------------------------------- */
    /*  12. SCROLL TOP BUTTON
    /* ----------------------------------------------------------- */

    //Check to see if the window is top if not then display button

    jQuery(window).scroll(function () {
        if (jQuery(this).scrollTop() > 300) {
            jQuery('.scrollToTop').fadeIn();
        } else {
            jQuery('.scrollToTop').fadeOut();
        }
    });

    //Click event to scroll to top

    jQuery('.scrollToTop').click(function () {
        jQuery('html, body').animate({ scrollTop: 0 }, 800);
        return false;
    });


});
